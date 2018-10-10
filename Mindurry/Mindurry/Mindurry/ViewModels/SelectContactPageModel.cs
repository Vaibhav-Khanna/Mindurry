using FreshMvvm;
using Mindurry.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Mindurry.ViewModels.Base;
using System.Threading.Tasks;
using Mindurry.Models.DataObjects;
using Mindurry.Models;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Plugin.Messaging;
using Mindurry.Helpers;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SelectContactPageModel : BasePageModel
    {


        private IEnumerable<Contact> _contacts;
        public ObservableCollection<ContactsListModel> Contacts { get; set; }
        private List<ContactsListModel> contactsLoadData;
        private string Filter = null;

        private ContactsListModel selectedItem;
        [PropertyChanged.DoNotNotify]
        public ContactsListModel SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value != null)
                    // CoreMethods.PushPageModel<LeadDetailPageModel>(value.ContactId);
                    SendMailto(value);
                selectedItem = null;
                RaisePropertyChanged();
            }
        }
        private List<ResidenceModel> ApartmentListing;
        public IEnumerable<IGrouping<Residence, ResidenceModel>> GroupedItems { get; set; }

        public async override void Init(object initData)
        {
            base.Init(initData);

            ApartmentListing = (List<ResidenceModel>)initData;

            await LoadData();
        }


        public async Task LoadData()
        {

            _contacts = null;
            Contacts = null;
            long totalCount;

            _contacts = await StoreManager.ContactStore.GetItemsFilterAsync(Filter);         
            totalCount = (_contacts as IQueryResultEnumerable<Contact>).TotalCount;


            if ((_contacts != null) || (!_contacts.Any()))
            {
                var list = _contacts.ToList();
                list.AddRange(_contacts);

                //  var totalCount = (_contacts as IQueryResultEnumerable<Contact>).TotalCount;
                if (Convert.ToInt32(totalCount) - list.Count() > 0)
                    IsLoadMore = true;
                else
                    IsLoadMore = false;

                Contacts = new ObservableCollection<ContactsListModel>();
                var indexValue = 0; //to calculate Index to the backgroundColor
                                    // residences Names


                foreach (var item in _contacts)
                {

                    var contactListItem = new ContactsListModel();
                    contactListItem.Index = indexValue; // to alternante background Color

                    contactListItem.ContactId = item.Id;
                    contactListItem.Name = item.Firstname + " " + item.Lastname;
                    contactListItem.Email = item.Email;
                    contactListItem.Telephone = item.Phone;


                    indexValue++; //increment to change Backgroung Color
                    Contacts.Add(contactListItem);
                }
                contactsLoadData = Contacts.ToList();
            }
            else
            {
                await CoreMethods.DisplayAlert("Erreur", "Impossibilité de charger les données", "OK");
            }
        }

        bool Loadingmore = false;
        //Fetch more contacts for infinite scroll
        public Command LoadMore => new Command(async () =>
        {
            if (!IsLoadMore || Loadingmore)
                return;

            Loadingmore = true;
            long totalCount;


            _contacts = await StoreManager.ContactStore.GetNextItemsFilterAsync(Contacts.Count, Filter);
            totalCount = (_contacts as IQueryResultEnumerable<Contact>).TotalCount;



            if ((_contacts != null) || (!_contacts.Any()))
            {


                Contacts = new ObservableCollection<ContactsListModel>();
                var indexValue = 0; //to calculate Index to the backgroundColor


                foreach (var item in _contacts)
                {

                    var contactListItem = new ContactsListModel();
                    contactListItem.Index = indexValue; // to alternante background Color
                    contactListItem.ContactId = item.Id;

                    contactListItem.Name = item.Firstname + " " + item.Lastname;
                    contactListItem.Email = item.Email;
                    contactListItem.Telephone = item.Phone;

                    indexValue++; //increment to change Backgroung Color
                    Contacts.Add(contactListItem);
                }

                var list = contactsLoadData;
                list.AddRange(Contacts);


                if (Convert.ToInt32(totalCount) - list.Count() > 0)
                {
                    IsLoadMore = true;
                }
                else
                {
                    IsLoadMore = false;
                }
            }
            else
            {
                IsLoadMore = false;
                await CoreMethods.DisplayAlert("Erreur", "Impossibilité de charger les données", "OK");
            }
            Loadingmore = false;
        });

        public Command SearchCommand => new Command<string>(async (searchString) =>
        {

            if (searchString?.Length > 0)
            {
                searchString = searchString.ToLower();
                Filter = searchString;

            }
            else
            {
                Filter = null;
            }
            await LoadData();
        });

        private async void SendMailto(ContactsListModel contact)
        {
            List<Models.DataObjects.Apartment> apartmentList = new List<Models.DataObjects.Apartment>();

            GroupedItems = ApartmentListing.GroupBy(x => x.Residence);
            /*
            string body= "<p>Bonjour,</p><p>Veuillez trouver ci-joint les informations sur nos appartements.</p>";

            foreach (var group in GroupedItems)
            {
                body += "<p><b>Résidence " + group.Key.Name + " </b>";
                body += " (<a href=\"Link\">Plaquette</a>) </p>";
                body += "<p>";
                foreach (ResidenceModel r in group)
                {
                    body += "- <b>Appartement n° " + r.Apartment.LotNumberArchitect + " - "+ r.Apartment.Kind + " > </b><a href=\"Link\"> Plan PDF</a></br> ";
                    body += "";
                }
                body += "</p>";
            }
            body += "<p>Cordialement,</p>";
            */
            string body = "<b>Re-Bonjour</b> " + contact.Name + ", \n\nVeuillez trouver ci-dessous les informations sur nos résidences et appartements.\n\n";

            foreach (var group in GroupedItems)
            {

                
                body += "Résidence " + group.Key.Name + " sur " + group.Key.Locality+ ":\n\n" ;

                //Plan
                var residenceDocs = await StoreManager.DocumentMindurryStore.GetItemsByKindAndReferenceIdAsync(group.Key.Id, ReferenceKind.Residence.ToString().ToLower());

                foreach (var item in residenceDocs)
                {
                    body += item.Name + " ";
                    body += new Uri($"{Constants.ApplicationURL}/api/documentMindurry/{item.Id}/plan");
                    body += "\n";

                }
               
                body += "\n\n";
                foreach (ResidenceModel r in group)
                {
                    // Plan appartement
                    var apartmentDocs = await StoreManager.DocumentMindurryStore.GetItemsByKindAndReferenceIdAsync(r.Apartment.Id, ReferenceKind.Apartment.ToString().ToLower());
                    DocumentMindurry validDoc = null;
                    var finals = apartmentDocs.Where(x => x.DocumentType == DocumentType.Definitif.ToString().ToLower());
                    if (finals != null && finals.Any())
                    {
                        validDoc = finals.First();
                    }
                    else
                    {
                        var signs = apartmentDocs.Where(x => x.DocumentType == DocumentType.Signe.ToString().ToLower());
                        if (signs != null && signs.Any())
                        {
                            validDoc = signs.First();
                        }
                        else
                        {
                            var initials = apartmentDocs.Where(x => x.DocumentType == DocumentType.Initial.ToString().ToLower());
                            if (initials != null && initials.Any())
                            {
                                validDoc = initials.First();
                            }
                        }
                    }


                        body += "- Appartement n° " + r.Apartment.LotNumberArchitect + " - " + r.Apartment.Kind + "-> " + validDoc.Name + " ";
                        body += new Uri($"{Constants.ApplicationURL}/api/documentMindurry/{validDoc.Id}/plan");
                }
                body += "\n\n\n";
            }
            body += "Nous vous rappelons que ces appartements seront livrés à nos clients pour le  .​​......\n\n";
            body += "Le bâtiment est conforme à la construction BBC, vous permettant de bénéficier d'une réduction d'impôt (dans le cadre d'un investissement locatif sous la loi PINEL).\n";
            body += "Attention, les prix indiqués s'entendent hors ........\n\n";
            body += "Vous souhaitant une bonne réception,\n";
            body += "Cordialement,";

            try
            {

                var builder = new EmailMessageBuilder()
                 .To(contact.Email)
                 .Subject("Mindurry Promotion Exemple");

                var emailMessenger = CrossMessaging.Current.EmailMessenger;
                if (emailMessenger.CanSendEmail)
                {
                    builder.Body(body);
                    var email = builder.Build();
                    emailMessenger.SendEmail(email);
                }
                 DependencyService.Get<ISave>().CopyToClipboard(body);
               
                /*
                if (emailMessenger.CanSendEmailBodyAsHtml)
                {
                    builder.BodyAsHtml(body);
                    var email = builder.Build();
                    emailMessenger.SendEmail(email);               
                } */

                /*
                 *  List<string> recipients = new List<string> { contactEmail };
                var message = new EmailMessage
                {
                    Subject = "[Mindurry Promotion] Proposition d'appartements",
                    Body = body,
                    To = recipients,
                    BodyFormat= EmailBodyFormat.Html
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
              await Email.ComposeAsync(message); 
            }*/

            }
            
            catch (Exception ex)
            {
                // Some other exception occurred 
            }


        }
    }
}
