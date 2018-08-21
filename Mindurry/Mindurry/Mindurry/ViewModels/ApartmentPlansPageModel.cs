using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ApartmentPlansPageModel : BasePageModel
	{
        public Apartment Apartment { get; set; }

        public DocumentMindurry Initial { get; set; }
        public DocumentMindurry Sign { get; set; }
        public DocumentMindurry Final { get; set; }
        public DocumentMindurry Electric { get; set; }
        public DocumentMindurry Kitchen { get; set; }
        public DocumentMindurry Choice { get; set; }
        public ObservableCollection<DocumentMindurry>  Others { get; set; }

    public async override void Init(object initData)
        {
            base.Init(initData);

            Apartment = (Apartment)initData;

            //Documents
            var docs = await StoreManager.DocumentMindurryStore.GetItemsByKindAndReferenceIdAsync(Apartment.Id, ReferenceKind.Apartment.ToString().ToLower());
            if (docs != null && docs.Any())
            {

                Initial = docs.Where(x => x.DocumentType == DocumentType.Initial.ToString().ToLower()).First();

                Sign = docs.Where(x => x.DocumentType == DocumentType.Signe.ToString().ToLower()).First();

                Final = docs.Where(x => x.DocumentType == DocumentType.Definitif.ToString().ToLower()).First();

                Electric = docs.Where(x => x.DocumentType == DocumentType.Electrique.ToString().ToLower()).First();

                Kitchen = docs.Where(x => x.DocumentType == DocumentType.Cuisine.ToString().ToLower()).First();

                Choice = docs.Where(x => x.DocumentType == DocumentType.Choix.ToString().ToLower()).First();

                Others = new ObservableCollection<DocumentMindurry>(docs.Where(x => x.DocumentType == DocumentType.Autre.ToString().ToLower()));
            }
        }
    }
}