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
using Xamarin.Essentials;

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
        public ContactsListModel SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value != null)
                    // CoreMethods.PushPageModel<LeadDetailPageModel>(value.ContactId);
                    SendEmail(value.Email);
                selectedItem = null;
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

        private async void SendEmail(string contactEmail)
        {
            List<Models.DataObjects.Apartment> apartmentList = new List<Models.DataObjects.Apartment>();

            GroupedItems = ApartmentListing.GroupBy(x => x.Residence);

            string body="";

            foreach (var group in GroupedItems)
            {
                body += "<h1>Résidence " + group.Key.Name + " </h1>";
                body += "<a href=\"Link\">Plan</a>";

                foreach (ResidenceModel r in group)
                {
                    body += "<p> <h2>Appartement n° " + r.Apartment.LotNumberArchitect + "</h2>";
                    body += "<a href=\"Link\">Plan</a></p>";
                }
            }

            try
            {
                List<string> recipients = new List<string> { contactEmail };
                var message = new EmailMessage
                {
                    Subject = "[Mindurry Promotion] Proposition d'appartements",
                    Body = body,
                    To = recipients,
                    // BodyFormat= EmailBodyFormat.Html
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
              await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }


        }
    }
}
