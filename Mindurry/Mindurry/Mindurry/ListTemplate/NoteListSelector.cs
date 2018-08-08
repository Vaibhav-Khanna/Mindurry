using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.ListTemplate
{
    public class NoteListSelector : DataTemplateSelector
    {

        public DataTemplate ContactTemplate { get; set; }
        public DataTemplate CommercialTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((((Note)item).Kind != "mailReceived") && (((Note)item).Kind != "mailOpened")) ? CommercialTemplate : ContactTemplate;
        }



    }
}
