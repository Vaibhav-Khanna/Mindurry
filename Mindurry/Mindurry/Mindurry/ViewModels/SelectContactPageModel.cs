using FreshMvvm;
using Mindurry.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SelectContactPageModel : FreshBasePageModel
    {
        public ObservableCollection<CheckablePerson> Items { get; set; }

        public Residence SelectedItem
        {
            get => null;
            set
            { }
        }

        public bool IsSelectionOn { get; set; } = false;

        public override void Init(object initData)
        {
            base.Init(initData);

            var item1 = new CheckablePerson
            {
                Name = "John Doe",
                Email = "j.doe@gmail.com",
                Telephone = "09 36 73 83 83",
                Commercial = "Arold Martino"
            };

            var item2 = new CheckablePerson
            {
                Name = "Sullivan Marc",
                Email = "m.sullivan@immo.com",
                Telephone = "06 87 76 44 56",
                Commercial = "Jean Noosa"
            };

            var item3 = new CheckablePerson
            {
                Name = "Marie Yuji",
                Email = "sean.yuji@yuji.com",
                Telephone = "07 56 65 63 00",
                Commercial = "Jean Noosa"
            };

            var item4 = new CheckablePerson
            {
                Date = new DateTime(2017, 12, 11, 9, 34, 0),
                Name = "Albert Louak",
                Email = "m.louak@tera.net",
                Telephone = "06 67 55 87 99",
                Commercial = "Jean Noosa"
            };

            var item5 = new CheckablePerson
            {
                Date = new DateTime(2017, 9, 12, 11, 59, 0),
                Name = "Louis Aroati",
                Email = "franck.aroati@immo.com",
                Telephone = "07 67 55 22 78",
                Commercial = "Jean Noosa"
            };

            Items = new ObservableCollection<CheckablePerson> { item1, item2, item3, item4, item5 };

            ViewModels.StaticViewModel.SelectionChanged += StaticViewModel_SelectionChanged;
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            ViewModels.StaticViewModel.SelectionChanged -= StaticViewModel_SelectionChanged;
        }

        private void StaticViewModel_SelectionChanged(object sender, EventArgs e)
        {
            IsSelectionOn = Items.Any(x => x.IsChecked);
        }
    }
}
