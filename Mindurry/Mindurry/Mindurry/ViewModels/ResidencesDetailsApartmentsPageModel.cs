using Mindurry.DataModels;
using Mindurry.Models;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidencesDetailsApartmentsPageModel : BasePageModel
    {
        public ObservableCollection<CheckBoxItem> TypesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> ExpositionChecks { get; set; }

        public ObservableCollection<ApartmentsListModel> Apartments { get; set; }
        private List<ApartmentsListModel> OriginalApartments;

        private ApartmentsListModel selectedApartment;
        public ApartmentsListModel SelectedApartment
        {
            get => selectedApartment;
            set
            {
                if (value != null)
                     
                    App.RequestApartmentTabbedPage(value.Apartment);
                selectedApartment = null;
            }
        }
        private string residenceId;
        bool _terraceChecked;
        public bool TerraceChecked { get { return _terraceChecked; } set { _terraceChecked = value; Filter(); RaisePropertyChanged(); } }

        bool _gardenChecked;
        public bool GardenChecked { get { return _gardenChecked; } set { _gardenChecked = value; Filter(); RaisePropertyChanged(); } }


        public bool IsSecondListVisible { get; set; } = true;
        public bool IsThirdListVisible { get; set; } = true;
        public bool IsFilterOn { get; set; } = false;

        public string ArrowTwo
        {
            get => IsSecondListVisible ? "" : "";
        }

        public string ArrowThree
        {
            get => IsThirdListVisible ? "" : "";
        }

        public ICommand ShowFilterCommand { get; set; }
        public ICommand ArrowTwoCommand { get; set; }
        public ICommand ArrowThreeCommand { get; set; }
        public ICommand ClearAllFilterCommand { get; set; }
        

        public async override void Init(object initData)
        {
            base.Init(initData);

            var data = (string)initData;

            residenceId = data;

            await LoadApartments();

             LoadFilters();

            ShowFilterCommand = new Command(ShowFilter);
            ArrowTwoCommand = new Command(ChangeArrowTwo);
            ArrowThreeCommand = new Command(ChangeArrowThree);
            ClearAllFilterCommand = new Command(ClearAllFilters);
        }

        public async Task LoadApartments()
        {
            Apartments = new ObservableCollection<ApartmentsListModel>();
            var apartments = await StoreManager.ApartmentStore.GetItemsByResidenceId(residenceId);
            if (apartments != null && apartments.Any())
            {
                apartments.OrderBy(x => x.LotNumberArchitect);
                foreach (var item in apartments)
                {
                    var apartmentsListItem = new ApartmentsListModel
                    {
                        Apartment = item
                    };
                    // Client
                    if (!String.IsNullOrEmpty(item.ContactId))
                    {
                        var acquereur = await StoreManager.ContactStore.GetItemAsync(item.ContactId);
                        apartmentsListItem.Client = acquereur.Name;
                    }
                    // Garden
                    var gardens = await StoreManager.GardenStore.GetGardensByResidenceId(residenceId,item.Id);
                    if (gardens != null && gardens.Any())
                    {
                        long gardenArea = 0;
                        foreach (var garden in gardens)
                        {
                            gardenArea += garden.Area;
                        }
                        apartmentsListItem.GardenArea = gardenArea;
                    }
                    // Terrace
                    var terraces = await StoreManager.TerraceStore.GetTerracesByResidenceId(residenceId, item.Id);
                    if (terraces != null && terraces.Any())
                    {
                        long terraceArea = 0;
                        foreach (var terrace in terraces)
                        {
                            terraceArea += terrace.Area;
                        }
                        apartmentsListItem.TerraceArea = terraceArea;
                    }

                    Apartments.Add(apartmentsListItem);
                    OriginalApartments = Apartments.ToList();
                }
            }
        }

        public void LoadFilters()
        {
            TypesChecks = new ObservableCollection<CheckBoxItem>();
            ExpositionChecks = new ObservableCollection<CheckBoxItem>();
            // Type Checkbox Items
            foreach (var item in Apartments.DistinctBy(s => s.Apartment.Kind).OrderBy(s => s.Apartment.Kind).ToList())
            {
                CheckBoxItem check;
                TypesChecks.Add(check = new CheckBoxItem() { Content = item.Apartment.Kind, DataType = CheckBoxContainerDataType.Type });
                check.PropertyChanged += TypeFilterChanged;
            }

            // Exposure Checkbox Items
            foreach (var item in Apartments.DistinctBy(s => s.Apartment.Exposure).OrderBy(s => s.Apartment.Exposure).ToList())
            {
                CheckBoxItem check;
                ExpositionChecks.Add(check = new CheckBoxItem() { Content = item.Apartment.Exposure, DataType = CheckBoxContainerDataType.Exposure });
                check.PropertyChanged += TypeFilterChanged;
            }
        }
        void ClearTypeChecks()
        {
            foreach (var item in TypesChecks)
            {
                item.IsChecked = false;
            }
        }

        void ClearExposureChecks()
        {
            foreach (var item in ExpositionChecks)
            {
                item.IsChecked = false;
            }
        }
        void ClearAllFilters()
        {
            ClearTypeChecks();
            ClearExposureChecks();

            TerraceChecked = false;
            GardenChecked = false;
        }

        void TypeFilterChanged(object sender, PropertyChangedEventArgs e)
        {
            var type = sender as CheckBoxItem;

            Filter();
        }

        void ExposureFilterChanged(object sender, PropertyChangedEventArgs e)
        {
            var exposure = sender as CheckBoxItem;

            Filter();
        }
        void Filter()
        {
            var filter_list = new List<ApartmentsListModel>();

            filter_list = OriginalApartments;

            //Filtered for types
            var types_checked = TypesChecks.Where(r => r.IsChecked).ToList();
            if (types_checked.Any())
            {
                var temp_list = new List<ApartmentsListModel>();

                foreach (var item in types_checked)
                {
                    temp_list.AddRange(filter_list.Where(x => x.Apartment.Kind == item.Content));
                }

                filter_list = temp_list;
            }
            
            //Filtered for exposure
            var exposure_checked = ExpositionChecks.Where(x => x.IsChecked).ToList();
            if (exposure_checked.Any())
            {
                var temp_list = new List<ApartmentsListModel>();

                foreach (var item in exposure_checked)
                {
                    temp_list.AddRange(filter_list.Where(x => x.Apartment.Exposure == item.Content));
                }

                filter_list = temp_list;
            }

            //Garden switch filter
            if (GardenChecked)
                filter_list = filter_list.Where(r => r.GardenArea != 0).ToList();

            //Terrace switch filter
            if (TerraceChecked)
                filter_list = filter_list.Where(r => r.TerraceArea != 0).ToList();


            Apartments = new ObservableCollection<ApartmentsListModel>(filter_list);
        }


        void ShowFilter()
        {
            IsFilterOn = !IsFilterOn;
        }

        void ChangeArrowTwo()
        {
            IsSecondListVisible = !IsSecondListVisible;
        }

        void ChangeArrowThree()
        {
            IsThirdListVisible = !IsThirdListVisible;
        }
    }
}
