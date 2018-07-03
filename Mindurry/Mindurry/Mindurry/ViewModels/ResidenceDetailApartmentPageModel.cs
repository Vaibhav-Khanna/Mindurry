using Mindurry.DataModels;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidenceDetailApartmentPageModel : BasePageModel
    {
        public ObservableCollection<Residence> Items { get; set; }
        public ObservableCollection<CheckBoxItem> TypesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> ExpositionChecks { get; set; }

        public Residence SelectedItem
        {
            get => null;
            set
            {
                if (value != null)
                    CoreMethods.PushPageModel<ViewModels.ResidencesDetailsAppartementsInfosPageModel>(value);
            }
        }

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

            Items = new ObservableCollection<Residence> { item1, item2, item3, item4 };

            var check4 = new CheckBoxItem { Content = "Studio" };
            var check5 = new CheckBoxItem { Content = "T2", IsChecked = true };
            var check6 = new CheckBoxItem { Content = "T3" };

            TypesChecks = new ObservableCollection<CheckBoxItem> { check4, check5, check6 };

            var check7 = new CheckBoxItem { Content = "Nord" };
            var check8 = new CheckBoxItem { Content = "Sud" };
            var check9 = new CheckBoxItem { Content = "Est" };

            ExpositionChecks = new ObservableCollection<CheckBoxItem> { check7, check8, check9 };

            ShowFilterCommand = new Command(ShowFilter);
            ArrowTwoCommand = new Command(ChangeArrowTwo);
            ArrowThreeCommand = new Command(ChangeArrowThree);
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
