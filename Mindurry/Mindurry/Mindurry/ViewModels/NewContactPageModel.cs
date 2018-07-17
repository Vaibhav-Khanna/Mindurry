using FreshMvvm;
using Mindurry.Models;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NewContactPageModel : BasePageModel
    {
        public List<string> Picker1Items { get; set; }
        public List<string> Picker2Items { get; set; }
        public List<string> Picker3Items { get; set; }

        public int SelectedIndex1 { get; set; }
        public int SelectedIndex2 { get; set; }
        public int SelectedIndex3 { get; set; }

        public string Address { get; set; }

        private double? _latitude;
        private double? _longitude;
        private string _streetNumber;
        private string _street;
        private string _locality;
        private string _postalCode;
        private string _country;

        public List<string> CollectSourcesName { get; set; }

        public string CollectSourcesSelectedName { get; set; }

        public List<string> CustomFieldsName { get; set; }

        public string CustomFieldsSelectedName { get; set; }
        

        public async override void Init(object initData)
        {
            base.Init(initData);
            await FetchCollectSources();

            await FetchType();
        }


        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
         if (returnedData is Tuple<string, double, double, List<AddressComponent>>)
            {
                Address = ((Tuple<string, double, double, List<AddressComponent>>)returnedData).Item1;
                _latitude = ((Tuple<string, double, double, List<AddressComponent>>)returnedData).Item2;
                _longitude = ((Tuple<string, double, double, List<AddressComponent>>)returnedData).Item3;
                var AddressComponents = ((Tuple<string, double, double, List<AddressComponent>>)returnedData).Item4;


                for (var i = 0; i < AddressComponents.Count; i++)
                {

                    for (var j = 0; j < AddressComponents[i].Types.Count; j++)
                    {
                        if (AddressComponents[i].Types[j] == "street_number") { _streetNumber = AddressComponents[i].LongName; }
                        if (AddressComponents[i].Types[j] == "route") { _street = AddressComponents[i].LongName; }
                        if (AddressComponents[i].Types[j] == "locality") { _locality = AddressComponents[i].LongName; }
                        if (AddressComponents[i].Types[j] == "country") { _country = AddressComponents[i].LongName; }
                        if (AddressComponents[i].Types[j] == "postal_code") { _postalCode = AddressComponents[i].LongName; }

                    }

                }

            }
        }

        public Command SaveCommand => new Command(async () =>
        {
                      

        });

        public Command AddressCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<AddressPickPageModel>(null, true);
        });

        async Task FetchCollectSources()
        {
            var collectSource = await StoreManager.CollectSourceStore.GetItemsAsync();

            CollectSourcesName = new List<string>();

            foreach (CollectSource item in collectSource)
            {
                CollectSourcesName.Add(item.Name);
            }
        if (CollectSourcesName.Any())
            {
                CollectSourcesSelectedName = CollectSourcesName[0];
            }
            
        }

        async Task FetchType()
        {
            var customField = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("98c3e3eb-07dc-4a7b-b222-17fe859ddf6e");
                CustomFieldsName = new List<string>();

            foreach (ContactCustomFieldSourceEntry item in customField)
            {
                CustomFieldsName.Add(item.Value);
            }
            if (CustomFieldsName.Any())
            {
                CustomFieldsSelectedName = CustomFieldsName[0];
            }
        }

    }
}
