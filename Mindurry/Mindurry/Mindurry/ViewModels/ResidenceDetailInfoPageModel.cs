using Mindurry.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidenceDetailInfoPageModel : FreshMvvm.FreshBasePageModel
    {
        public ObservableCollection<IconTitle> Items { get; set; }

        public Residence SelectedItem
        {
            get => null;
            set
            { }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            var item1 = new IconTitle { Icon = "icon_PDF.png", Title = "Plaquette" };
            var item2 = new IconTitle { Icon = "icon_PDF.png", Title = "Plan de masse" };
            var item3 = new IconTitle { Icon = "icon_PDF.png", Title = "Notice" };
            var item4 = new IconTitle { Icon = "icon_PDF.png", Title = "Plan / Niveau" };
            var item5 = new IconTitle { Icon = "icon_PDF.png", Title = "Plan en perspective" };

            Items = new ObservableCollection<IconTitle> { item1, item2, item3, item4, item5 };
        }
    }
}
