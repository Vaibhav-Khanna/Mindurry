using FreshMvvm;
using Mindurry.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class LeadDetailPageModel : FreshBasePageModel
    {
        Lead Item { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
            Item = (Lead)initData;
        }
    }
}
