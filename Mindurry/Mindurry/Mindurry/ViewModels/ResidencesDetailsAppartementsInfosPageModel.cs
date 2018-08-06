using Mindurry.DataModels;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidencesDetailsAppartementsInfosPageModel : BasePageModel
    {
        public ResidenceModel Item { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            Item = (ResidenceModel)initData;
           

        }

        
    }
}
