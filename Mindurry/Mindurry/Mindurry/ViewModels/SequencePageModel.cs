﻿using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xam.Plugin.WebView.Abstractions;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    public class SequencePageModel : BasePageModel
    {
        private string ContactId;
        public string Url { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
            ContactId = (string)initData;

            Url = "https://crm-sequencemail-dev.azurewebsites.net/web/";
            
        }

        public Command CloseCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(true);

        });
    }
}
