using FreshMvvm;
using Mindurry.DataModels;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ClientsDetailsInfoPageModel : BasePageModel
    {
        public Contact Item { get; set; }
        public ObservableCollection<Activity> Activities { get; set; }

        public ObservableCollection<string> Combo1 { get; set; }
        public ObservableCollection<string> ComboL1 { get; set; }
        public ObservableCollection<string> ComboL2 { get; set; }
        public ObservableCollection<string> ComboL3 { get; set; }
        public ObservableCollection<string> Types { get; set; }
        public ObservableCollection<DateTitle> Documents { get; set; }
        public ObservableCollection<DataModels.Residence> Residences { get; set; }

        public int TabIndex { get; set; }
        public int TabTwoLevel { get; set; }
        public int TabThreeLevel { get; set; }

        public bool NotifyService { get; set; }

        public bool IsTabOneVisible
        {
            get => TabIndex == 0;
        }

        public bool IsTabTwoVisible
        {
            get => TabIndex == 1;
        }

        public bool IsTabThreeVisible
        {
            get => TabIndex == 2;
        }

        public Color TabOneColor
        {
            get => TabIndex == 0 ? Color.FromHex("9f182c") : Color.FromRgb(101, 101, 101);
        }

        public Color TabTwoColor
        {
            get => TabIndex == 1 ? Color.FromHex("9f182c") : Color.FromRgb(101, 101, 101);
        }

        public Color TabThreeColor
        {
            get => TabIndex == 2 ? Color.FromHex("9f182c") : Color.FromRgb(101, 101, 101);
        }

        public bool IsTabTwoL1 => TabTwoLevel == 0;
        public bool IsTabTwoL2 => TabTwoLevel == 1;
        public bool IsTabTwoL3 => TabTwoLevel == 2;

        public bool IsTabThreeL1 => TabThreeLevel == 0;
        public bool IsTabThreeL2 => TabThreeLevel == 1;
        public bool IsTabThreeL3 => TabThreeLevel == 2;

        public string Combo1Selected { get; set; }
        public string ComboL1Selected { get; set; }
        public string ComboL2Selected { get; set; }
        public string ComboL3Selected { get; set; }
        public string TypeSelected { get; set; }
        public bool IsFirstListVisible { get; set; } = true;
        public bool IsSecondListVisible { get; set; } = true;
        public string ArrowOne
        {
            get => IsFirstListVisible ? "" : "";
        }

        public string ArrowTwo
        {
            get => IsSecondListVisible ? "" : "";
        }

        public ICommand TabOneCommand { get; set; }
        public ICommand TabTwoCommand { get; set; }
        public ICommand TabThreeCommand { get; set; }
        public ICommand TabThreeBackCommand { get; set; }
        public ICommand TabThreeForwardCommand { get; set; }
        public ICommand TabTwoBackCommand { get; set; }
        public ICommand TabTwoForwardCommand { get; set; }

        public ICommand AddDocumentCommand { get; set; }

        public ICommand ArrowOneCommand { get; set; }
        public ICommand ArrowTwoCommand { get; set; }
        public ICommand ClearAllResidencesCommand { get; set; }
        public ICommand ClearAllTypesCommand { get; set; }

        public ObservableCollection<CheckBoxItem> ResidencesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> TypesChecks { get; set; }
        public ObservableCollection<string> Combo4 { get; set; }
        public string Combo4Selected { get; set; }
        public override void Init(object initData)
        {
            base.Init(initData);
            Item = (Contact)initData;

            var activity1 = new Activity
            {
                Date = new DateTime(2018, 2, 21, 15, 5, 0),
                Name = "Manuel  Llop",
                Message = "Prise de contact effectué, rdv pris pour le vendredi 25",
                Icon = "",
                Color = Color.FromRgb(249, 249, 249),
                HorizontalOptions = Xamarin.Forms.LayoutOptions.End
            };

            var activity2 = new Activity
            {
                Date = new DateTime(2018, 2, 20, 15, 12, 0),
                Name = "John Doe",
                Message = "Bonjour, je souhaiterais en savoir plus sur le programme villa Aguiléra. Merci pour votre aide !",
                Icon = "",
                Color = Color.FromRgb(231, 243, 255),
                HorizontalOptions = Xamarin.Forms.LayoutOptions.Start
            };

            Activities = new ObservableCollection<Activity> { activity1, activity2 };

            Combo4 = new ObservableCollection<string> { "Arold Martino", "Jean Noosa" };


            Types = new ObservableCollection<string> { "Investisseur" };
            Combo1 = new ObservableCollection<string> { "Lead" };
            ComboL1 = new ObservableCollection<string> { "Herrian" };
            ComboL2 = new ObservableCollection<string> { "Appartement" };
            ComboL3 = new ObservableCollection<string> { "---" };

            Combo1Selected = Combo1[0];
            ComboL1Selected = ComboL1[0];
            ComboL2Selected = ComboL2[0];
            ComboL3Selected = ComboL3[0];
            Combo4Selected = Combo4[0];
            TypeSelected = Types[0];

            var item1 = new CheckBoxItem { Content = "Herrian" };
            var item2 = new CheckBoxItem { Content = "Herri Ondo", IsChecked = true };
            var item3 = new CheckBoxItem { Content = "Villa Aguiléra" };

            ResidencesChecks = new ObservableCollection<CheckBoxItem> { item1, item2, item3 };

            var item4 = new CheckBoxItem { Content = "Studio" };
            var item5 = new CheckBoxItem { Content = "T2", IsChecked = true };
            var item6 = new CheckBoxItem { Content = "T3" };

            TypesChecks = new ObservableCollection<CheckBoxItem> { item4, item5, item6 };

            var doc1 = new DateTitle
            {
                Date = new DateTime(2018, 2, 12),
                Title = "Compromis de vente"
            };

            var doc2 = new DateTitle
            {
                Date = new DateTime(2018, 1, 10),
                Title = "Plan séjour"
            };

            var doc3 = new DateTitle
            {
                Date = new DateTime(2018, 1, 8),
                Title = "Plan global"
            };

            var doc4 = new DateTitle
            {
                Date = new DateTime(2018, 12, 12),
                Title = "Contrat"
            };

            Documents = new ObservableCollection<DateTitle> { doc1, doc2, doc3, doc4 };

            var residence1 = new DataModels.Residence()
            {
                NoArchi = 456,
                Type = "T3",
                Surface = 86,
                Prix = 126000,
                Stade = "En attente des options",
                ResidenceType = ResidenceType.Apartment,            
                SelectedStatus = Statut.Reserve
            };

            Residences = new ObservableCollection<DataModels.Residence> { residence1 };

            ArrowOneCommand = new Command(ChangeArrowOne);
            ArrowTwoCommand = new Command(ChangeArrowTwo);

            ClearAllResidencesCommand = new Command(ClearAllResidences);
            ClearAllTypesCommand = new Command(ClearAllTypes);

            TabOneCommand = new Command(TabOne);
            TabTwoCommand = new Command(TabTwo);
            TabThreeCommand = new Command(TabThree);

            TabThreeBackCommand = new Command(TabThreeBack);
            TabThreeForwardCommand = new Command(TabThreeForward);

            TabTwoBackCommand = new Command(TabTwoBack);
            TabTwoForwardCommand = new Command(TabTwoForward);
        }

        void ChangeArrowOne()
        {
            IsFirstListVisible = !IsFirstListVisible;
        }

        void ChangeArrowTwo()
        {
            IsSecondListVisible = !IsSecondListVisible;
        }

        void ClearAllResidences()
        {
            foreach (var item in ResidencesChecks.ToArray())
            {
                item.IsChecked = false;
            }
        }

        void ClearAllTypes()
        {
            foreach (var item in TypesChecks.ToArray())
            {
                item.IsChecked = false;
            }
        }

        void TabOne()
        {
            TabIndex = 0;
        }

        void TabTwo()
        {
            TabIndex = 1;
        }

        void TabThree()
        {
            TabIndex = 2;
        }

        void TabTwoBack()
        {
            TabTwoLevel--;
        }

        void TabTwoForward()
        {
            TabTwoLevel++;
        }

        void TabThreeBack()
        {
            TabThreeLevel--;
        }

        void TabThreeForward()
        {
            TabThreeLevel++;
        }
    }
}
