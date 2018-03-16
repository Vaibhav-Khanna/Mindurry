using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class DashboardPageModel : FreshBasePageModel
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Test { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
            EndDate = DateTime.Today;
            StartDate = DateTime.Today.AddMonths(-1);
        }
    }
}
