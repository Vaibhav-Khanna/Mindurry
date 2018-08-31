using FreshMvvm;
using Mindurry.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Mindurry.ViewModels.Base;
using Mindurry.Models.DataObjects;
using System.Diagnostics;
using System.ComponentModel;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidencesPageModel : BasePageModel
    {
        public IEnumerable<IGrouping<Residence, ResidenceModel>> GroupedItems { get; set; }

        private ObservableCollection<ResidenceModel> AllResidences { get; set; }
        private ObservableCollection<ResidenceModel> FilteredResidences { get; set; }

        public ObservableCollection<CheckBoxItem> ResidencesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> TypesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> ExpositionChecks { get; set; }

        private ResidenceModel selectedItem;
        public ResidenceModel SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value != null)
                {
                    RequestTabbedPage(value.Apartment);
                }
               
                selectedItem = null;
            }
        }

        private string _searchText;
        public string SearchText { get { return _searchText; } set { _searchText = value; Search(); RaisePropertyChanged(); } }

        public bool IsShareButtonVisible { get; set; } = false;
        public bool IsFirstListVisible { get; set; } = true;
        public bool IsSecondListVisible { get; set; } = true;
        public bool IsThirdListVisible { get; set; } = true;
        public bool IsFilterOn { get; set; } = false;

        bool _terraceChecked;
        public bool TerraceChecked { get { return _terraceChecked; } set { _terraceChecked = value; Search(); RaisePropertyChanged(); } }

        bool _gardenChecked;
        public bool GardenChecked { get { return _gardenChecked; } set { _gardenChecked = value; Search(); RaisePropertyChanged(); } }

        public string ArrowOne
        {
            get => IsFirstListVisible ? "" : "";
        }

        public string ArrowTwo
        {
            get => IsSecondListVisible ? "" : "";
        }

        public string ArrowThree
        {
            get => IsThirdListVisible ? "" : "";
        }

        public ICommand ShowFilterCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand ArrowOneCommand { get; set; }
        public ICommand ArrowTwoCommand { get; set; }
        public ICommand ArrowThreeCommand { get; set; }      
        public ICommand ClearAllResidencesCommand { get; set; }
        public ICommand ClearAllTypesCommand { get; set; }
        public ICommand ClearAllExpositionCommand { get; set; }
        public ICommand ClearAllFilterCommand { get; set; }

        public async override void Init(object initData)
        {
            base.Init(initData);

            AllResidences = new ObservableCollection<ResidenceModel>();

            var residences = await StoreManager.ResidenceStore.GetAllActiveResidences();

            foreach (var _res in residences)
            {
                var _apartments = await StoreManager.ApartmentStore.GetApartmentsByResidenceId(_res.Id);

                if (_apartments != null)
                    foreach (var _apt in _apartments)
                    {
                        var _terraces = await StoreManager.TerraceStore.GetTerracesByResidenceId(_res.Id, _apt.Id);
                        var _gardens = await StoreManager.GardenStore.GetGardensByResidenceId(_res.Id, _apt.Id);

                        var _res_model = new ResidenceModel(_apt, _res);

                        if (_terraces != null && _terraces.Any())
                            foreach (var t in _terraces)
                            {
                                _res_model.Terace += t.Area;
                            }

                        if (_gardens != null && _gardens.Any())
                            foreach (var g in _gardens)
                            {
                                _res_model.Garden += g.Area;
                            }

                        AllResidences.Add(_res_model);
                    }
            }
           
            GroupedItems = AllResidences.GroupBy(x => x.Residence);

            ResidencesChecks = new ObservableCollection<CheckBoxItem>();

            // Residence checkbox Items
            foreach (var _res in residences)
            {
                CheckBoxItem item;
                ResidencesChecks.Add(item = new CheckBoxItem() { Content = _res.Name, Id = _res.Id, DataType = CheckBoxContainerDataType.Residence });
                item.PropertyChanged += ResidenceFilterChanged;
            }

            TypesChecks = new ObservableCollection<CheckBoxItem>();
            ExpositionChecks = new ObservableCollection<CheckBoxItem>();

            // Type Checkbox Items
            foreach (var item in AllResidences.DistinctBy(s=> s.Type).OrderBy(s=>s.Type).ToList())
            {
                CheckBoxItem check;
                TypesChecks.Add(check = new CheckBoxItem() { Content = item.Type, DataType = CheckBoxContainerDataType.Type });
                check.PropertyChanged += TypeFilterChanged;
            }

            // Exposure Checkbox Items
            foreach (var item in AllResidences.DistinctBy(s=>s.Exposure).OrderBy(s=>s.Exposure).ToList())
            {
                CheckBoxItem check;
                ExpositionChecks.Add(check = new CheckBoxItem() { Content = item.Exposure, DataType = CheckBoxContainerDataType.Exposure });
                check.PropertyChanged += TypeFilterChanged;
            }
            
            ShowFilterCommand = new Command(ShowFilter);
            ShareCommand = new Command(Share);
            ArrowOneCommand = new Command(ChangeArrowOne);
            ArrowTwoCommand = new Command(ChangeArrowTwo);
            ArrowThreeCommand = new Command(ChangeArrowThree);
            ClearAllResidencesCommand = new Command(ClearResidenceChecks);
            ClearAllTypesCommand = new Command(ClearTypeChecks);
            ClearAllExpositionCommand = new Command(ClearExposureChecks);
            ClearAllFilterCommand = new Command(ClearAllFilters);

            ViewModels.StaticViewModel.SelectionChanged += StaticViewModel_SelectionChanged;
        }       

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            ViewModels.StaticViewModel.SelectionChanged -= StaticViewModel_SelectionChanged;
        }

        private void StaticViewModel_SelectionChanged(object sender, EventArgs e)
        {
            foreach (var group in GroupedItems)
            {
                foreach (ResidenceModel r in group)
                {
                    if (r.IsChecked)
                    {
                        IsShareButtonVisible = true;
                        return;
                    }
                }
            }
            IsShareButtonVisible = false;
        }

        void ShowFilter()
        {
            IsFilterOn = !IsFilterOn;
        }

        void Share()
        {
            CoreMethods.PushPageModel<SelectContactPageModel>();
        }

        void ChangeArrowOne()
        {
            IsFirstListVisible = !IsFirstListVisible;
        }

        void ChangeArrowTwo()
        {
            IsSecondListVisible = !IsSecondListVisible;
        }

        void ChangeArrowThree()
        {
            IsThirdListVisible = !IsThirdListVisible;
        }

        void ClearResidenceChecks()
        {
            foreach (var item in ResidencesChecks)
            {
                item.IsChecked = false;
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
            ClearResidenceChecks();
            ClearTypeChecks();
            ClearExposureChecks();

            TerraceChecked = false;
            GardenChecked = false;
        }

        void Search()
        {
            Filter();

            if (string.IsNullOrWhiteSpace(SearchText))
                GroupedItems = FilteredResidences.GroupBy(x => x.Residence);
            else
                GroupedItems = FilteredResidences.Where(x => x.Parent.ToLower().Contains(SearchText.ToLower())).GroupBy(x => x.Residence);
        }

        void ResidenceFilterChanged(object sender, PropertyChangedEventArgs e)
        {
            var residence = sender as CheckBoxItem;

            Search();
        }

        void TypeFilterChanged(object sender, PropertyChangedEventArgs e)
        {
            var type = sender as CheckBoxItem;

            Search();
        }

        void ExposureFilterChanged(object sender, PropertyChangedEventArgs e)
        {
            var exposure = sender as CheckBoxItem;

            Search();
        }

        void Filter()
        {
            var filter_list = new List<ResidenceModel>();

            // Filtered for residence
            var residences_checked = ResidencesChecks.Where(r => r.IsChecked).ToList();
            if (residences_checked.Any())
            {
                var temp_list = new List<ResidenceModel>();

                foreach (var item in residences_checked)
                {
                    temp_list.AddRange(AllResidences.Where(r => r.Parent == item.Content));
                }

                filter_list = temp_list.Distinct().ToList();             
            }
            else
                filter_list = AllResidences.ToList();

            //Filtered for types
            var types_checked = TypesChecks.Where(r => r.IsChecked).ToList();
            if (types_checked.Any())
            {
                var temp_list = new List<ResidenceModel>();

                foreach (var item in types_checked)
                {
                    temp_list.AddRange(filter_list.Where(r => r.Type == item.Content));
                }

                filter_list = temp_list;
            }

            //Filtered for exposure
            var exposure_checked = ExpositionChecks.Where(r => r.IsChecked).ToList();
            if (exposure_checked.Any())
            {
                var temp_list = new List<ResidenceModel>();

                foreach (var item in exposure_checked)
                {
                    temp_list.AddRange(filter_list.Where(r => r.Exposure == item.Content));
                }

                filter_list = temp_list;
            }

            //Garden switch filter
            if (GardenChecked)         
                filter_list = filter_list.Where(r => r.Garden != 0).ToList();

            //Terrace switch filter
            if (TerraceChecked)
                filter_list = filter_list.Where(r => r.Terace != 0).ToList();


            FilteredResidences = new ObservableCollection<ResidenceModel>(filter_list);
        }

        private async void RequestTabbedPage(Models.DataObjects.Apartment apt)
        {
            var title = apt.ResidenceName + " > " + "Appartement " + apt.LotNumberArchitect.ToString();

            var page_1 = FreshPageModelResolver.ResolvePageModel<ApartmentDetailInfoPageModel>(apt);
            page_1.Title = "Informations";

            var page_2 = FreshPageModelResolver.ResolvePageModel<ApartmentPlansPageModel>(apt);
            page_2.Title = "Plans";

            var tabbed_page = new TabbedPage() { Title = title };

            tabbed_page.Children.Add(page_1);
            tabbed_page.Children.Add(page_2);

            await this.CurrentPage.Navigation.PushAsync(tabbed_page);
        }
    }
}
