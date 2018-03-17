using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.ViewModels
{
    public class NewContactPageModel : FreshBasePageModel
    {
        public List<string> Picker1Items { get; set; }
        public List<string> Picker2Items { get; set; }

        public int SelectedIndex1 { get; set; }
        public int SelectedIndex2 { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            Picker1Items = new List<string> { "Lead" };
            Picker2Items = new List<string> { "Tags - multiselect" };

            SelectedIndex1 = 0;
            SelectedIndex2 = 0;
        }
    }
}
