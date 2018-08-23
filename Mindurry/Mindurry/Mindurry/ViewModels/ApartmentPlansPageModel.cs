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

        public DocumentMindurry Initial { get; set; } = new DocumentMindurry();
        public DocumentMindurry Sign { get; set; } = new DocumentMindurry();
        public DocumentMindurry Final { get; set; } = new DocumentMindurry();
        public DocumentMindurry Electric { get; set; } = new DocumentMindurry();
        public DocumentMindurry Kitchen { get; set; } = new DocumentMindurry();
        public DocumentMindurry Choice { get; set; } = new DocumentMindurry();
        public ObservableCollection<DocumentMindurry>  Others { get; set; } 

    public async override void Init(object initData)
        {
            base.Init(initData);

            Apartment = (Apartment)initData;

            //Documents
            var docs = await StoreManager.DocumentMindurryStore.GetItemsByKindAndReferenceIdAsync(Apartment.Id, ReferenceKind.Apartment.ToString().ToLower());
            if (docs != null && docs.Any())
            {

                var initials = docs.Where(x => x.DocumentType == DocumentType.Initial.ToString().ToLower());
                if (initials != null && initials.Any())
                {
                    Initial = initials.First();
                }

                var signs = docs.Where(x => x.DocumentType == DocumentType.Signe.ToString().ToLower());
                if (signs != null && signs.Any())
                {
                    Sign = signs.First();
                }

                var finals = docs.Where(x => x.DocumentType == DocumentType.Definitif.ToString().ToLower());
                if (finals != null && finals.Any())
                {
                    Final = finals.First();
                }

                var electrics = docs.Where(x => x.DocumentType == DocumentType.Electrique.ToString().ToLower());
                if (electrics != null && electrics.Any())
                {
                    Electric = electrics.First();
                }

                var kitchens = docs.Where(x => x.DocumentType == DocumentType.Cuisine.ToString().ToLower());
                if (kitchens != null && kitchens.Any())
                {
                    Kitchen = kitchens.First();
                }

                var choices = docs.Where(x => x.DocumentType == DocumentType.Choix.ToString().ToLower());
                if (choices != null && choices.Any())
                {
                    Choice = choices.First();
                }

                Others = new ObservableCollection<DocumentMindurry>(docs.Where(x => x.DocumentType == DocumentType.Autre.ToString().ToLower()));
            }
        }

        public Command DisplayPlanCommand => new Command<DocumentMindurry>(async (obj) =>
        {
            await CoreMethods.PushPageModel<PdfDisplayPageModel>(obj, true);


        });
    }
}