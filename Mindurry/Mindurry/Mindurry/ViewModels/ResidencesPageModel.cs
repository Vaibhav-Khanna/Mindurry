
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
        public ObservableCollection<CheckBoxItem> CommandStatesChecks { get; set; }

        private ResidenceModel selectedItem;
        [PropertyChanged.DoNotNotify]
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
                RaisePropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText { get { return _searchText; } set { _searchText = value; if (!resetAll) { Search(); }; RaisePropertyChanged(); } }

        public bool IsShareButtonVisible { get; set; } = false;
        public bool IsFirstListVisible { get; set; } = true;
        public bool IsSecondListVisible { get; set; } = true;
        public bool IsThirdListVisible { get; set; } = true;
        public bool IsFourListVisible { get; set; } = true;
        public bool IsFilterOn { get; set; } = false;

        bool _terraceChecked;
        [PropertyChanged.DoNotNotify]
        public bool TerraceChecked {get {return _terraceChecked; }set{_terraceChecked = value;if (!resetAll) { Search(); }; RaisePropertyChanged();} }

        bool _gardenChecked;
        [PropertyChanged.DoNotNotify]
        public bool GardenChecked {get { return _gardenChecked; }set {_gardenChecked = value; if (!resetAll) { Search(); }; RaisePropertyChanged();} }

        bool _garageChecked;
        [PropertyChanged.DoNotNotify]
        public bool GarageChecked {get { return _garageChecked; }set { _garageChecked = value; if (!resetAll) { Search(); }; RaisePropertyChanged();} }

        bool _cellarChecked;
        [PropertyChanged.DoNotNotify]
        public bool CellarChecked {get { return _cellarChecked; }set { _cellarChecked = value; if (!resetAll) { Search(); }; RaisePropertyChanged(); } }


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

        public string ArrowFor
        {
            get => IsFourListVisible ? "" : "";
        }

        public float ResMaximumPrice { get; set; }
        public float ResMinimumPrice { get; set; }

        public float ResMaximumArea { get; set; }
        public float ResMinimumArea { get; set; }

        float _resUpperAreaValue;
        [PropertyChanged.DoNotNotify]
        public float ResUpperAreaValue
        {
            get
            {
                return _resUpperAreaValue;
            }
            set
            {
                _resUpperAreaValue = value;
                if (_resUpperAreaValue <= ResMaximumArea && _resUpperAreaValue > 0 && resetAll==false)
                {
                    Search();
                }

                RaisePropertyChanged();
            }
        }
        float _resLowerAreaValue;
        [PropertyChanged.DoNotNotify]
        public float ResLowerAreaValue
        {
            get
            {
                return _resLowerAreaValue;
            }
            set
            {
                _resLowerAreaValue = value;
                if (_resLowerAreaValue >= ResMinimumArea && resetAll == false)
                {
                    Search();
                }
                RaisePropertyChanged();
            }
        }

        float _resUpperPriceValue;
        [PropertyChanged.DoNotNotify]
        public float ResUpperPriceValue
        {
            get
            {
                return _resUpperPriceValue;
            }
            set
            {
                _resUpperPriceValue = value;
                if (_resUpperPriceValue <= ResMaximumPrice && _resUpperPriceValue > 0 && resetAll == false)
                {
                    Search();
                }

                RaisePropertyChanged();
            }
        }
        float _resLowerPriceValue;
        [PropertyChanged.DoNotNotify]
        public float ResLowerPriceValue
        {
            get
            {
                return _resLowerPriceValue;
            }
            set
            {
                _resLowerPriceValue = value;
                if (_resLowerPriceValue >= ResMinimumPrice && resetAll == false)
                {
                    Search();
                }
                RaisePropertyChanged();
            }
        }


        public ICommand ShowFilterCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand ArrowOneCommand { get; set; }
        public ICommand ArrowTwoCommand { get; set; }
        public ICommand ArrowThreeCommand { get; set; }
        public ICommand ArrowFourCommand { get; set; }
        public ICommand ClearAllResidencesCommand { get; set; }
        public ICommand ClearAllTypesCommand { get; set; }
        public ICommand ClearAllExpositionCommand { get; set; }
        public ICommand ClearAllFilterCommand { get; set; }

        IEnumerable<Residence> residences;
        private bool resetAll { get; set; } = false;

        public async override void Init(object initData)
        {
            base.Init(initData);

            AllResidences = new ObservableCollection<ResidenceModel>();

             residences = await StoreManager.ResidenceStore.GetAllActiveResidences();

            foreach (var _res in residences)
            {
                var _apartments = await StoreManager.ApartmentStore.GetApartmentsByResidenceId(_res.Id);
                
                if (_apartments != null) { 
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
                        // Client
                        if (!String.IsNullOrEmpty(_apt.ContactId))
                        {
                            var acquereur = await StoreManager.ContactStore.GetItemAsync(_apt.ContactId);
                            if (acquereur != null) { 
                                _res_model.Client = acquereur.Name;
                            }
                        }

                        AllResidences.Add(_res_model);
                    }
                }
            }
           
            GroupedItems = AllResidences.GroupBy(x => x.Residence);
           

            ResidencesChecks = new ObservableCollection<CheckBoxItem>();

            // Residence checkbox Items
            foreach (var _res in GroupedItems)
            {
                CheckBoxItem item;
                ResidencesChecks.Add(item = new CheckBoxItem() { Content = _res.Key.Name, Id = _res.Key.Id, DataType = CheckBoxContainerDataType.Residence });
                item.PropertyChanged += ResidenceFilterChanged;
            }

            TypesChecks = new ObservableCollection<CheckBoxItem>();
            ExpositionChecks = new ObservableCollection<CheckBoxItem>();
            CommandStatesChecks = new ObservableCollection<CheckBoxItem>();

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
                check.PropertyChanged += ExposureFilterChanged;
            }

            // CommandState Checkbox Items
            foreach (var item in AllResidences.DistinctBy(s => s.State).OrderBy(s => s.State).ToList())
            {
                CheckBoxItem check;
                CommandStatesChecks.Add(check = new CheckBoxItem() { Content = item.State, DataType = CheckBoxContainerDataType.CommandState });
                check.PropertyChanged += CommandStatesFilterChanged;
            }

            if (AllResidences != null && AllResidences.Any())
            {
                //Price Filter
                ResMaximumPrice = AllResidences.OrderByDescending(a => a.Apartment.Price).First().Apartment.Price;
                ResUpperPriceValue = ResMaximumPrice;
                ResMinimumPrice = AllResidences.OrderBy(a => a.Apartment.Price).First().Apartment.Price;
                ResLowerPriceValue = ResMinimumPrice;            

                //Area Filter
                ResMaximumArea = AllResidences.OrderByDescending(b => b.Apartment.Area).First().Apartment.Area;
                ResUpperAreaValue = ResMaximumArea;
                ResMinimumArea = AllResidences.OrderBy(b => b.Apartment.Area).First().Apartment.Area;
                ResLowerAreaValue = ResMinimumArea;
                
            }

            ShowFilterCommand = new Command(ShowFilter);
            ShareCommand = new Command(Share);
            ArrowOneCommand = new Command(ChangeArrowOne);
            ArrowTwoCommand = new Command(ChangeArrowTwo);
            ArrowThreeCommand = new Command(ChangeArrowThree);
            ArrowFourCommand = new Command(ChangeArrowFour);
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

            List<ResidenceModel> apartmentList = new List<ResidenceModel>();
            foreach (var group in GroupedItems)
            {
                
                foreach (ResidenceModel r in group)
                {
                    if (r.IsChecked)
                    {
                        apartmentList.Add(r);
                    }
                }
            }

            CoreMethods.PushPageModel<SelectContactPageModel>(apartmentList);
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

        void ChangeArrowFour()
        {
            IsFourListVisible = !IsFourListVisible;
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

        void ClearCommandStatesChecks()
        {
            foreach (var item in CommandStatesChecks)
            {
                item.IsChecked = false;
            }
        }

        void ClearAllFilters()
        {
            resetAll = true;

            // nettoyage des checksBox
            ClearResidenceChecks();
            ClearTypeChecks();
            ClearExposureChecks();
            ClearCommandStatesChecks();

            // remise à niveau des prix 
            ResLowerAreaValue = ResMinimumArea;
            ResUpperAreaValue = ResMaximumArea;
            //remise à niveau des area
            ResLowerPriceValue = ResMinimumPrice;
            ResUpperPriceValue = ResMaximumPrice;

            
            // remise à false pour les swtich choisi
            if (TerraceChecked == true) { TerraceChecked = false; }
            if (GardenChecked == true) { GardenChecked = false; }
            if (GarageChecked == true){ GarageChecked = false; }
            if (CellarChecked == true){ CellarChecked = false; }

            //remise à jour de la liste
            Search();

            resetAll = false;
        }

        async void Search()

        {
            if (resetAll) {
                GroupedItems = AllResidences.GroupBy(x => x.Residence);
            }
            else
            {
                await Filter();

                if (string.IsNullOrWhiteSpace(SearchText))
                    GroupedItems = FilteredResidences.GroupBy(x => x.Residence);
                else
                    GroupedItems = FilteredResidences.Where(x => x.Parent.ToLower().Contains(SearchText.ToLower())).GroupBy(x => x.Residence);
            }
            
        }

        void ResidenceFilterChanged(object sender, PropertyChangedEventArgs e)
        {
            var residence = sender as CheckBoxItem;
            if (!resetAll)
            {
                Search();
            }
           
        }

        void TypeFilterChanged(object sender, PropertyChangedEventArgs e)
        {
            var type = sender as CheckBoxItem;
            if (!resetAll)
            {
                Search();
            }
        }

        void ExposureFilterChanged(object sender, PropertyChangedEventArgs e)
        {
            var exposure = sender as CheckBoxItem;
            if (!resetAll)
            {
                Search();
            }
        }

        void CommandStatesFilterChanged(object sender, PropertyChangedEventArgs e)
        {
            var commandState = sender as CheckBoxItem;
            if (!resetAll)
            {
                Search();
            }
        }

        async Task Filter()
        {
            var filter_list = new List<ResidenceModel>();

            // Filtered for residence
            if (ResidencesChecks != null)
            {
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
            }

            //Filtered for types
            if (TypesChecks != null)
            {
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
            }

            //Filtered for exposure
            if (ExpositionChecks != null)
            {
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
            }
            //Filtered for State
            if (CommandStatesChecks != null)
            {
                var states_checked = CommandStatesChecks.Where(r => r.IsChecked).ToList();
                if (states_checked.Any())
                {
                    var temp_list = new List<ResidenceModel>();

                    foreach (var item in states_checked)
                    {
                        temp_list.AddRange(filter_list.Where(r => r.State == item.Content));
                    }

                    filter_list = temp_list;
                }
            }

            //Garden switch filter
            if (GardenChecked)         
                filter_list = filter_list.Where(r => r.Garden != 0).ToList();

            //Terrace switch filter
            if (TerraceChecked)
                filter_list = filter_list.Where(r => r.Terace != 0).ToList();

            //Garage switch filter
            if (GarageChecked)
            {
                var temporary_list = new List<ResidenceModel>();
                // verif si garage dispo ds residence
                foreach (Residence resi in residences)
                {
                    bool isGarage =  await StoreManager.GarageStore.IsStillGarageInResidence(resi.Id);
                    if (isGarage == true)
                    {
                       // filter_list = filter_list.Where(r => r.Residence.Id != resi.Id).ToList();
                        temporary_list.AddRange(filter_list.Where(r => r.Residence.Id == resi.Id));
                    }
                    
                }
                filter_list = temporary_list;

            }
                

            //cellar switch filter
            if (CellarChecked)
            {
                var t_list = new List<ResidenceModel>();
                // verif si garage dispo ds residence
                foreach (Residence resi in residences)
                {
                    bool isCellar = await StoreManager.CellarStore.IsStillCellarInResidence(resi.Id);
                    if (isCellar == true)
                    {
                        // filter_list = filter_list.Where(r => r.Residence.Id != resi.Id).ToList();
                        t_list.AddRange(filter_list.Where(r => r.Residence.Id == resi.Id));
                    }

                }
                filter_list = t_list;
            }
                

            if (ResLowerAreaValue != ResUpperAreaValue && ResUpperAreaValue > 0)
            {
                filter_list = filter_list.Where(a => (a.Apartment.Area >= ResLowerAreaValue && a.Apartment.Area <= ResUpperAreaValue)).ToList();
            }
            if (ResLowerPriceValue != ResUpperPriceValue && ResUpperPriceValue > 0)
            {
                filter_list = filter_list.Where(a => (a.Apartment.Price >= ResLowerPriceValue && a.Apartment.Price <= ResUpperPriceValue)).ToList();
            }
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
