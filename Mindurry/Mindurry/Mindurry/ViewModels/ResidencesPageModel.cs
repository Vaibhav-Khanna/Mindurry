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

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidencesPageModel : BasePageModel
    {
        public IEnumerable<IGrouping<string, Residence>> GroupedItems { get; set; }
       
        public ObservableCollection<CheckBoxItem> ResidencesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> TypesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> ExpositionChecks { get; set; }

        public Residence SelectedItem
        {
            get => null;
            set
            { }
        }

        public bool IsShareButtonVisible { get; set; } = false;
        public bool IsFirstListVisible { get; set; } = true;
        public bool IsSecondListVisible { get; set; } = true;
        public bool IsThirdListVisible { get; set; } = true;
        public bool IsFilterOn { get; set; } = false;

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

        public override void Init(object initData)
        {
            base.Init(initData);

            var item1 = new Residence
            {
                Parent = "Herrian",
                NoArchi = 678,
                Type = "T2",
                Exposition = "Nord",
                Surface = 89,
                Terasse = 14,
                Jardin = 0,
                Client = "John Doe",
                Stade = "-",
                Prix = 168000
            };

            var item2 = new Residence
            {
                Parent = "Herrian",
                NoArchi = 456,
                Type = "T2",
                Exposition = "Nord",
                Surface = 67,
                Terasse = 14,
                Jardin = 0,
                Client = "Marc Duix",
                Stade = "-",
                Prix = 130000
            };

            var item3 = new Residence
            {
                Parent = "Herrian",
                NoArchi = 5678,
                Type = "T3",
                Exposition = "Nord",
                Surface = 56,
                Terasse = 14,
                Jardin = 0,
                Client = "Marie Marto",
                Stade = "-",
                Prix = 156000
            };

            var item4 = new Residence
            {
                Parent = "Herrian",
                NoArchi = 567,
                Type = "T4",
                Exposition = "Nord",
                Surface = 34,
                Terasse = 14,
                Jardin = 0,
                Client = "Henri Lapuie",
                Stade = "Opts validés",
                Prix = 110500
            };

            var item21 = new Residence
            {
                Parent = "Herri Ondo",
                NoArchi = 5678,
                Type = "T2",
                Exposition = "Nord",
                Surface = 109,
                Terasse = 14,
                Jardin = 0,
                Client = "John Doe",
                Stade = "-",
                Prix = 230000
            };

            var item22 = new Residence
            {
                Parent = "Herri Ondo",
                NoArchi = 777,
                Type = "T3",
                Exposition = "Nord",
                Surface = 87,
                Terasse = 14,
                Jardin = 0,
                Client = "Marc Duix",
                Stade = "-",
                Prix = 197000
            };

            var item31 = new Residence
            {
                Parent = "Miragarri",
                NoArchi = 234,
                Type = "T2",
                Exposition = "Nord",
                Surface = 89,
                Terasse = 14,
                Jardin = 25,
                Client = "Marc Duix",
                Stade = "-",
                Prix = 206000
            };

            var item32 = new Residence
            {
                Parent = "Miragarri",
                NoArchi = 86,
                Type = "T4",
                Exposition = "Nord",
                Surface = 48,
                Terasse = 14,
                Jardin = 0,
                Client = "Marc Duix",
                Stade = "-",
                Prix = 136000
            };

            var item33 = new Residence
            {
                Parent = "Miragarri",
                NoArchi = 67,
                Type = "T4",
                Exposition = "Nord",
                Surface = 44,
                Terasse = 14,
                Jardin = 0,
                Client = "Marc Duix",
                Stade = "-",
                Prix = 178000
            };

            var item34 = new Residence
            {
                Parent = "Miragarri",
                NoArchi = 78,
                Type = "T3",
                Exposition = "Nord",
                Surface = 67,
                Terasse = 14,
                Jardin = 0,
                Client = "Marc Duix",
                Stade = "-",
                Prix = 168000
            };

            var item41 = new Residence
            {
                Parent = "Villa Aguilera",
                NoArchi = 657,
                Type = "T3",
                Exposition = "Nord",
                Surface = 89,
                Terasse = 14,
                Jardin = 0,
                Client = "Marc Duix",
                Stade = "-",
                Prix = 198000
            };

            var item42 = new Residence
            {
                Parent = "Villa Aguilera",
                NoArchi = 576,
                Type = "T4",
                Exposition = "Nord",
                Surface = 78,
                Terasse = 14,
                Jardin = 0,
                Client = "Marc Duix",
                Stade = "-",
                Prix = 154000
            };

            var items = new ObservableCollection<Residence> { item1, item2, item21, item22, item3, item31, item32, item33, item34, item4, item41, item42 };
            GroupedItems = items.GroupBy(x => x.Parent);

            var check1 = new CheckBoxItem { Content = "Herrian" };
            var check2 = new CheckBoxItem { Content = "Herri Ondo", IsChecked = true };
            var check3 = new CheckBoxItem { Content = "Villa Aguiléra" };

            ResidencesChecks = new ObservableCollection<CheckBoxItem> { check1, check2, check3 };

            var check4 = new CheckBoxItem { Content = "Studio" };
            var check5 = new CheckBoxItem { Content = "T2", IsChecked = true };
            var check6 = new CheckBoxItem { Content = "T3" };

            TypesChecks = new ObservableCollection<CheckBoxItem> { check4, check5, check6 };

            var check7 = new CheckBoxItem { Content = "Nord" };
            var check8 = new CheckBoxItem { Content = "Sud" };
            var check9 = new CheckBoxItem { Content = "Est" };

            ExpositionChecks = new ObservableCollection<CheckBoxItem> { check7, check8, check9 };

            ShowFilterCommand = new Command(ShowFilter);
            ShareCommand = new Command(Share);
            ArrowOneCommand = new Command(ChangeArrowOne);
            ArrowTwoCommand = new Command(ChangeArrowTwo);
            ArrowThreeCommand = new Command(ChangeArrowThree);

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
                foreach (Residence r in group)
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
    }
}
