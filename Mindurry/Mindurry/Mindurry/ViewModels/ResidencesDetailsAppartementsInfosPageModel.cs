using Mindurry.DataModels;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidencesDetailsAppartementsInfosPageModel : BasePageModel
    {
        public Residence Item { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            Item = (Residence)initData;
        }
    }
}
