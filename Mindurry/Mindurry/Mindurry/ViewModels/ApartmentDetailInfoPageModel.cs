using Mindurry.DataModels;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ApartmentDetailInfoPageModel : BasePageModel
    {
        public ResidenceModel Item { get; set; }


        public override void Init(object initData)
        {
            base.Init(initData);
            
            Item = (ResidenceModel)initData;
          

        }
    }
}