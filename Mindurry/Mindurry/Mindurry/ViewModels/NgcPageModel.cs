using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.ViewModels
{
    public class NgcPageModel : BasePageModel
    {
        public string Url { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
          
            Url = Constants.NgcApp + Helpers.Settings.AuthToken;

        }
    }
}
