using Acr.UserDialogs;
using FreshMvvm;
using Mindurry.DataModels;
using Mindurry.Helpers;
using Mindurry.Models;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ClientsDetailsInfoPageModel : BasePageModel
    {
        private string ContactId;
        public Contact Contact { get; set; }

        public ObservableCollection<RemindersCheckBoxListModel> Reminders { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        private ObservableCollection<Note> OriginalNotes;
        public Boolean ButtonShowMoreIsDisplayed { get; set; }
        public Boolean ButtonShowLessIsDisplayed { get; set; } = false;
        public DateTimeOffset? DateReminder { get; set; }
        public DateTimeOffset MinDate { get; set; } = new DateTimeOffset(DateTimeOffset.Now.Date);
        public string TextNote { get; set; }

        public ObservableCollection<CollectSource> CollectSources { get; set; }

        CollectSource selectedSource;
        public CollectSource SelectedSource
        {
            get
            {
                return selectedSource;
            }
            set
            {
                if (value != selectedSource && value != null)
                {
                    SaveCollect(value.Id);
                }
                selectedSource = value;
            }
        }
        public ObservableCollection<string> Qualifications { get; set; }
        private string selectedQualification;
        public string SelectedQualification
        {
            get
            {
                return selectedQualification;
            }
            set
            {
                if (value != selectedQualification && value != null)
                {
                    SaveQualification(value);
                }
                selectedQualification = value;
            }
        }

        public ObservableCollection<ContactCustomFieldSourceEntry> Types { get; set; }
        // public ContactCustomFieldSourceEntry SelectedType { get; set; }
        private ContactCustomFieldSourceEntry selectedType;
        public ContactCustomFieldSourceEntry SelectedType
        {
            get
            {
                return selectedType;
            }
            set
            {
                if (value != selectedType && value != null)
                {
                    SaveTypeContact(value.Id);
                }
                selectedType = value;
            }
        }


        public ObservableCollection<CheckBoxItem> ResidencesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> TypesChecks { get; set; }
        public ObservableCollection<User> Commercials { get; set; }
        private User selectedCommercial;
        public User SelectedCommercial
        {
            get
            {
                return selectedCommercial;
            }
            set
            {
                if (value != selectedCommercial && value != null)
                {
                    SaveCommercial(value.Id);
                }
                selectedCommercial = value;
            }
        }


        public ObservableCollection<Activity> Activities { get; set; }

        public ObservableCollection<Models.DataObjects.Residence> Residences { get; set; }
        
        private Models.DataObjects.Residence residenceSelected;
        public Models.DataObjects.Residence ResidenceSelected
         {
            get
            {
                return residenceSelected;
            }
            set
            {
                if (value != residenceSelected && value != null)
                {
                    References = null;
                    CalculReference(value.Id , TypeBienSelected);
                }
                residenceSelected = value;
            }
        }

        public ObservableCollection<string> Statuts { get; set; }
        public string StatutSelected { get; set; }

        public ObservableCollection<string> TypeBiens { get; set; }

        private string typeBienSelected;
        public string TypeBienSelected
        {
            get
            {
                return typeBienSelected;
            }
            set
            {
                if (value != typeBienSelected && value != null)
                {
                    References = null;
                    CalculReference(ResidenceSelected.Id ,value);
                }
                typeBienSelected = value;
            }
        }

        public ObservableCollection<string> References { get; set; }
        private string referenceSelected;
        public string ReferenceSelected
        {
            get
            {
                return referenceSelected;
            }
            set
            {
                if (value != referenceSelected && value != null)
                {                 
                    CalculPrice(value);
                }
                referenceSelected = value;
            }
        }

        public double? Price { get; set; }
        public ObservableCollection<ClientPropertyModel> PropertyList { get; set; }

        public ObservableCollection<DocumentMindurry> Documents { get; set; }
        public string FileName { get; set; }
        public bool RemoveButton { get; set; } = false;
        public FileData myTask { get; set; }
        public string DocumentName { get; set; }

        bool isSwitchOne;
        public bool IsSwitchOne
        {
            get { return isSwitchOne; }
            set
            {
                if (isSwitchOne != value)
                {
                    isSwitchOne = value;
                }
            }
        }

        bool isSwitchTwo;
        public bool IsSwitchTwo
        {
            get { return isSwitchTwo; }
            set
            {
                if (isSwitchTwo != value)
                {
                    isSwitchTwo = value;
                }
            }
        }
        bool isSwitchThree;
        public bool IsSwitchThree
        {
            get { return isSwitchThree; }
            set
            {
                if (isSwitchThree != value)
                {
                    isSwitchThree = value;
                }
            }
        }

        public string SequenceTitle { get; set; }

        public int TabIndex { get; set; }
        public int TabTwoLevel { get; set; }
        public int TabThreeLevel { get; set; }

        public bool NotifyService { get; set; }

        public bool IsTabOneVisible
        {
            get => TabIndex == 0;
        }

        public bool IsTabTwoVisible
        {
            get => TabIndex == 1;
        }

        public bool IsTabThreeVisible
        {
            get => TabIndex == 2;
        }

        public Color TabOneColor
        {
            get => TabIndex == 0 ? Color.FromHex("9f182c") : Color.FromRgb(101, 101, 101);
        }

        public Color TabTwoColor
        {
            get => TabIndex == 1 ? Color.FromHex("9f182c") : Color.FromRgb(101, 101, 101);
        }

        public Color TabThreeColor
        {
            get => TabIndex == 2 ? Color.FromHex("9f182c") : Color.FromRgb(101, 101, 101);
        }

        public bool IsTabTwoL1 => TabTwoLevel == 0;
        public bool IsTabTwoL2 => TabTwoLevel == 1;
        public bool IsTabTwoL3 => TabTwoLevel == 2;

        public bool IsTabThreeL1 => TabThreeLevel == 0;
        public bool IsTabThreeL2 => TabThreeLevel == 1;
       // public bool IsTabThreeL3 => TabThreeLevel == 2;

        public string TypeSelected { get; set; }
        public bool IsFirstListVisible { get; set; } = true;
        public bool IsSecondListVisible { get; set; } = true;
        public string ArrowOne
        {
            get => IsFirstListVisible ? "" : "";
        }

        public string ArrowTwo
        {
            get => IsSecondListVisible ? "" : "";
        }

        public ICommand TabOneCommand { get; set; }
        public ICommand TabTwoCommand { get; set; }
        public ICommand TabThreeCommand { get; set; }
        public ICommand TabThreeBackCommand { get; set; }
        public ICommand TabThreeForwardCommand { get; set; }
        public ICommand TabTwoBackCommand { get; set; }
        public ICommand TabTwoForwardCommand { get; set; }

        public ICommand AddDocumentCommand { get; set; }

        public ICommand ArrowOneCommand { get; set; }
        public ICommand ArrowTwoCommand { get; set; }
        public ICommand ClearAllResidencesCommand { get; set; }
        public ICommand ClearAllTypesCommand { get; set; }

        public bool IsReminders { get; set; }
        public bool IsSequence { get; set; }
        public bool IsTextNote { get; set; }

        public override async void Init(object initData)
        {
            base.Init(initData);
            ContactId = (string)initData;

            await LoadContact();

            await LoadReminders();

            await LoadNotes();

            await LoadFilters();

            //Sequence
            var result = await StoreManager.ContactSequenceStore.IsContactSequence(ContactId);
            if (result)
            {
                IsSequence = true;
            }
            else
            {
                IsSequence = false;
            }

            if (string.IsNullOrEmpty(TextNote))
            {
                IsTextNote = true;
            }
            else
            {
                IsTextNote = false;
            }

            //Reminder
            DateReminder = null;

            ArrowOneCommand = new Command(ChangeArrowOne);
            ArrowTwoCommand = new Command(ChangeArrowTwo);

            ClearAllResidencesCommand = new Command(ClearAllResidences);
            ClearAllTypesCommand = new Command(ClearAllTypes);

            TabOneCommand = new Command(TabOne);
            TabTwoCommand = new Command(TabTwo);
            TabThreeCommand = new Command(TabThree);

            TabThreeBackCommand = new Command(TabThreeBack);
            TabThreeForwardCommand = new Command(TabThreeForward);

            TabTwoBackCommand = new Command(TabTwoBack);
            TabTwoForwardCommand = new Command(TabTwoForward);
        }

        public async override void ReverseInit(object reverseData)
        {
            if (reverseData is Note)
            {
                await LoadReminders();
            }
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            PageWasPopped += ClientsDetailsInfoPageModel_PageWasPopped; ;
            if ((PreviousPageModel is ClientsPageModel))
            {

            }
        }

        private void ClientsDetailsInfoPageModel_PageWasPopped(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "ReloadCollection");
        }

        private async Task LoadContact()
        {
            Contact = await StoreManager.ContactStore.GetItemAsync(ContactId);
        }

        private async Task LoadReminders()
        {
            Reminders = new ObservableCollection<RemindersCheckBoxListModel>();
            var reminders = await StoreManager.NoteStore.GetNextRemindersByContactIdAsync(ContactId);
            if (reminders != null && reminders.Any())
            {
                foreach (var item in reminders)
                {
                    var reminderList = new RemindersCheckBoxListModel
                    {
                        Reminder = item,
                        IsChecked = false
                    };
                    Reminders.Add(reminderList);
                    IsReminders = true;
                }
            }
            else
            {
                IsReminders = false;
            }
        }


        private async Task LoadNotes()
        {
            var notes = await StoreManager.NoteStore.GetNotesByContactIdAsync(ContactId);
            if (notes != null && notes.Any())
            {
                OriginalNotes = new ObservableCollection<Note>(notes);
                if (OriginalNotes.Count > 3)
                {
                    Notes = new ObservableCollection<Note>(OriginalNotes.Take(3));
                    ButtonShowMoreIsDisplayed = true;
                }
                else
                {
                    Notes = new ObservableCollection<Note>(OriginalNotes);
                    ButtonShowMoreIsDisplayed = false;
                }
            }
            else
            {
                Notes = new ObservableCollection<Note>();
            }
        }

        private async Task LoadFilters()
        {
            // Source
            var collectSources = await StoreManager.CollectSourceStore.GetItemsAsync();
            if (collectSources != null && collectSources.Any())
            {
                CollectSources = new ObservableCollection<CollectSource>(collectSources);
                foreach (var item in CollectSources)
                {
                    if (item.Id == Contact.CollectSourceId)
                    {
                        SelectedSource = item;
                    }
                }

            }
            else
            {
                CollectSources = new ObservableCollection<CollectSource>();
            }

            // Qualification
            Qualifications = new ObservableCollection<string>(Enum.GetNames(typeof(Qualification)));
            foreach (var item in Qualifications)
            {
                if (item == Contact.Qualification)
                {
                    SelectedQualification = item;
                }
            }

            // Type
            var types = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Type");
            if (types != null && types.Any())
            {
                Types = new ObservableCollection<ContactCustomFieldSourceEntry>(types);
                var customType = (await StoreManager.ContactCustomFieldStore.GetItemsByContactCustomFieldSourceNameAndContactIdAsync("Type", ContactId)).ToList();
                if (customType != null && customType.Any())
                {
                    foreach (var item in Types)
                    {
                        if (customType[0].ContactCustomFieldSourceEntryId == item.Id)
                        {
                            SelectedType = item;
                        }
                    }
                }

            }
            else
            {
                Types = new ObservableCollection<ContactCustomFieldSourceEntry>();
            }

            // Residence
            var residences = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Résidences");
            residences.OrderBy(x => x.ContactCustomFieldSourceInternalName);

            ResidencesChecks = new ObservableCollection<CheckBoxItem>();
            foreach (var item in residences)
            {
                var customResidences = (await StoreManager.ContactCustomFieldStore.GetItemsByContactCustomFieldSourceNameAndContactIdAsync("Résidences", ContactId)).ToList();
                bool isCheck = false;

                if (customResidences != null && customResidences.Any())
                {
                    foreach (var customR in customResidences)
                    {
                        if (customR.ContactCustomFieldSourceEntryValue == item.Value)
                        {
                            isCheck = true;
                            break;
                        }
                    }
                }

                var resCheck = new CheckBoxItem
                {
                    Content = item.Value,
                    IsChecked = isCheck,
                    Id = item.Id
                };
                ResidencesChecks.Add(resCheck);
            }

            // Chargement type
            var typesApt = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Type d\'appartement");
            typesApt.OrderBy(x => x.ContactCustomFieldSourceInternalName);

            TypesChecks = new ObservableCollection<CheckBoxItem>();

            foreach (var item in typesApt)
            {
                var customTypes = (await StoreManager.ContactCustomFieldStore.GetItemsByContactCustomFieldSourceNameAndContactIdAsync("Type d\'appartement", ContactId)).ToList();
                bool isCheck = false;

                if (customTypes != null && customTypes.Any())
                {
                    foreach (var customT in customTypes)
                    {
                        if (customT.ContactCustomFieldSourceEntryValue == item.Value)
                        {
                            isCheck = true;
                            break;
                        }
                    }
                }
                var typeCheck = new CheckBoxItem
                {
                    Content = item.Value,
                    IsChecked = isCheck,
                    Id = item.Id
                };
                TypesChecks.Add(typeCheck);
            }


            // Commercials
            var commercials = await StoreManager.UserStore.GetItemsAsync();
            if (commercials != null && commercials.Any())
            {
                Commercials = new ObservableCollection<User>(commercials);
                foreach (var item in Commercials)
                {
                    if (item.Id == Contact.UserId)
                    {
                        SelectedCommercial = item;
                    }
                }
            }
            else
            {
                Commercials = new ObservableCollection<User>();
            }
        }
        private async void SaveCollect(string collectId)
        {
            Contact.CollectSourceId = collectId;
            await StoreManager.ContactStore.UpdateAsync(Contact);
        }

        private async void SaveQualification(string qualification)
        {
            Contact.Qualification = qualification;
            await StoreManager.ContactStore.UpdateAsync(Contact);
        }
        private async void SaveTypeContact(string contactCustomEntryId)
        {
            List<ContactCustomField> customField = (await StoreManager.ContactCustomFieldStore.GetItemsByContactCustomFieldSourceNameAndContactIdAsync("Type", ContactId)).ToList();
            if (customField != null && customField.Any())
            {
                customField[0].ContactCustomFieldSourceEntryId = contactCustomEntryId;
                await StoreManager.ContactCustomFieldStore.UpdateAsync(customField[0]);
            }
        }

        public Command SequenceCommand => new Command(async () => {

            await CoreMethods.PushPageModel<SequencePageModel>(ContactId, true);

        });

        public Command SaveChecksCommand => new Command<CheckBoxItem>(async (obj) =>
        {
            if (obj.IsChecked)
            {
                var ccfSourceEntry = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemAsync(obj.Id);

                ContactCustomField customToInsert = new ContactCustomField
                {
                    ContactId = ContactId,
                    ContactCustomFieldSourceEntryId = obj.Id,
                    ContactCustomFieldSourceId = ccfSourceEntry.ContactCustomFieldSourceId
                };

                await StoreManager.ContactCustomFieldStore.InsertAsync(customToInsert);
            }
            else
            {
                var customField = await StoreManager.ContactCustomFieldStore.GetItemByContactIdAndSourceEntryIdAsync(ContactId, obj.Id);
                await StoreManager.ContactCustomFieldStore.RemoveAsync(customField);
            }


        });

        private async void SaveCommercial(string commercialId)
        {
            Contact.UserId = commercialId;
            await StoreManager.ContactStore.UpdateAsync(Contact);
        }

        public Command UpdateContactCommand => new Command(async (obj) =>
        {
            await CoreMethods.PushPageModel<NewClientPageModel>(Contact.Id);
            SubUnsub();

        });

        public Command SelectReminderCommand => new Command<RemindersCheckBoxListModel>(async (obj) =>
        {
            if (obj.IsChecked)
            {
                var result = await CoreMethods.DisplayAlert("Classer", "Etes vous sur de vouloir terminer ce rappel ?", "Oui", "Non");
                if (result)
                {
                    using (UserDialogs.Instance.Loading("Classement du rappel", null, null, true))
                    {
                        RemindersCheckBoxListModel reminderObj = obj as RemindersCheckBoxListModel;
                        Note _note = reminderObj.Reminder;
                        _note.DoneAt = DateTimeOffset.Now;
                        _note.ActivityStreamDate = DateTimeOffset.Now;
                        await StoreManager.NoteStore.UpdateAsync(_note);
                        await LoadReminders();
                        await LoadNotes();
                    }
                }
                else { obj.IsChecked = false; }
            }
        });

        public Command UpdateReminderCommand => new Command<RemindersCheckBoxListModel>(async (obj) =>
        {
            await CoreMethods.PushPageModel<UpdateReminderPageModel>(obj.Reminder, true, false);

        });

        public Command DeleteReminderCommand => new Command<RemindersCheckBoxListModel>(async (obj) =>
        {
            var result = await CoreMethods.DisplayAlert("Suppression", "Etes vous sur de vouloir supprimer ce rappel ?", "Oui", "Non");
            if (result)
            {
                using (UserDialogs.Instance.Loading("Suppression du rappel", null, null, true))
                {
                    await StoreManager.NoteStore.RemoveAsync((Note)obj.Reminder);
                    await LoadReminders();
                }
            }
        });

        public Command AddNoteCommand => new Command(async (obj) =>
        {
            if (!string.IsNullOrEmpty(TextNote))
            {
                Note NoteToInsert = new Note()
                {
                    ContactId = ContactId,
                    UserId = Contact.UserId,
                    Kind = "note",
                    Content = TextNote
                };
                string title;
                if (DateReminder != null)
                {
                    NoteToInsert.ReminderAt = DateReminder;
                    title = "Ajout du rappel";
                }
                else
                {
                    title = "Ajout de la note";
                }
                using (UserDialogs.Instance.Loading(title, null, null, true))
                {
                    await StoreManager.NoteStore.InsertAsync(NoteToInsert);
                    TextNote = null;
                    if (DateReminder != null)
                    {
                        await LoadReminders();
                        DateReminder = null;
                    }
                    else
                    {
                        await LoadNotes();
                    }
                }
            }
            else
            {
                await CoreMethods.DisplayAlert("Erreur", "Il faut remplir un détail pour pouvoir enregistrer", "Ok");
            }
        });

        public Command DisplayMoreNotesCommand => new Command((obj) =>
        {
            Notes = OriginalNotes;
            ButtonShowMoreIsDisplayed = false;
            ButtonShowLessIsDisplayed = true;

        });
        public Command DisplayLessNotesCommand => new Command((obj) =>
        {
            Notes = new ObservableCollection<Note>(OriginalNotes.Take(3));
            ButtonShowMoreIsDisplayed = true;
            ButtonShowLessIsDisplayed = false;

        });

        void SubUnsub()
        {
            MessagingCenter.Subscribe<NewClientPageModel>(this, "ReloadDetailContact", async (obj) =>
            {

                await LoadContact();

                MessagingCenter.Unsubscribe<NewClientPageModel>(this, "ReloadDetailContact");
            });
        }

        void ChangeArrowOne()
        {
            IsFirstListVisible = !IsFirstListVisible;
        }

        void ChangeArrowTwo()
        {
            IsSecondListVisible = !IsSecondListVisible;
        }

        void ClearAllResidences()
        {
            foreach (var item in ResidencesChecks.ToArray())
            {
                item.IsChecked = false;
            }
        }

        void ClearAllTypes()
        {
            foreach (var item in TypesChecks.ToArray())
            {
                item.IsChecked = false;
            }
        }

        void TabOne()
        {
            TabIndex = 0;
        }

        async void TabTwo()
        {
            TabIndex = 1;
            await LoadDocuments();
        }

        async void TabThree()
        {
            TabIndex = 2;

            // Trouver les biens immobiliers de ce contacts
            await LoadProperties();

            // init formulaire (Ajouter un bien)
            var res = await StoreManager.ResidenceStore.GetItemsAsync();
            if (res != null && res.Any())
            {
                Residences = new ObservableCollection<Models.DataObjects.Residence>(res);
                ResidenceSelected = Residences[0];

                Statuts = new ObservableCollection<string> { "Option", "Réservation", "Signature" };
                StatutSelected = Statuts[0];
                TypeBiens = new ObservableCollection<string> { "Appartement", "Cave", "Parking" };
                TypeBienSelected = TypeBiens[0];

            }


        }

        void TabTwoBack()
        {
            TabTwoLevel--;
        }

        void TabTwoForward()
        {
            TabTwoLevel++;
        }

        void TabThreeBack()
        {
            TabThreeLevel--;
        }

         void TabThreeForward()
        {
            TabThreeLevel++;

        }

        private async Task LoadDocuments()
        {
            Documents = new ObservableCollection<DocumentMindurry>();
            // Recherche Appartements
            var documents = await StoreManager.DocumentMindurryStore.GetPostDocumentsByContactId(ContactId);
            if (documents != null && documents.Any())
            {
                Documents = new ObservableCollection<DocumentMindurry>(documents);
                //pull document to localStorage
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {                 
                        await StoreManager.DocumentMindurryStore.PullLatest(ContactId, ReferenceKind.Customer.ToString().ToLower());
                }
            }
            else
            {
                Documents = new ObservableCollection<DocumentMindurry>();
            }
        }
        public Command DownloadDocumentCommand => new Command<DocumentMindurry>(async (obj) => {
        StorageFolder folder;
           
                var fileName = obj.InternalName + "." + obj.Extension;
                var docDownloaded = await PclStorage.LoadFileLocal(fileName, ReferenceKind.Customer.ToString().ToLower(), obj.ReferenceId);

                var stream = new MemoryStream(docDownloaded);
                var name = obj.Name + "." + obj.Extension;

                folder = await DependencyService.Get<ISave>().Save(stream, name);
            if (folder != null) { 
                var result = await CoreMethods.DisplayAlert("Téléchargement", "Le téléchargement du document est terminé", "Ouvrir le répertoire", "Fermer");
                if (result)
                {
                    await DependencyService.Get<ISave>().LaunchFolder(folder);
                }
            }
        });

        public Command DeleteDocumentCommand => new Command(async (obj) => {

            var result = await CoreMethods.DisplayAlert("Suppression", "Etes vous sur de vouloir supprimer ce document ?", "Oui", "Non");
            if (result)
            {
                using (UserDialogs.Instance.Loading("Suppression du document", null, null, true))
                {
                    await StoreManager.DocumentMindurryStore.RemoveAsync((DocumentMindurry)obj);
                    await LoadDocuments();
                }
            }

        });

        public Command PickFileCommand => new Command(async (obj) =>
        {
                var crossFilePicker = Plugin.FilePicker.CrossFilePicker.Current;

                 myTask = await crossFilePicker.PickFile();

                if (!string.IsNullOrEmpty(myTask.FileName))
                   {
                    FileName = myTask.FileName;
                    RemoveButton = true;
                } 

         });
        public Command RemovePickFileCommand => new Command( () =>
        {
            myTask = null;
            FileName = null;
            RemoveButton = false;

        });

        public Command UploadFileCommand => new Command(async (obj) =>
        {

            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                if (FileName != null)
                {


                    if (!string.IsNullOrEmpty(DocumentName))
                    {
                        var validName = await StoreManager.DocumentMindurryStore.IsValidDocumentName(DocumentName);
                        if (validName) {
                            using (UserDialogs.Instance.Loading("Upload du document", null, null, true))
                            {
                                var is_uploaded = await StoreManager.DocumentMindurryStore.UploadDocument(myTask.DataArray, new DocumentMindurry() { Path = FileName, InternalName = Guid.NewGuid().ToString(), ReferenceKind = "customer", DocumentType = "autre", ReferenceId = ContactId, Name = DocumentName });
                                await LoadDocuments();
                                TabTwoBack();
                            }
                        }
                        else
                        {
                            await CoreMethods.DisplayAlert("Erreur", "Le nom entré est déjà utilisé pour un autre document", "Ok");
                        }
                    }
                    else
                    {
                        await CoreMethods.DisplayAlert("Erreur", "Il faut remplir un nom de document", "Ok");
                    }
                }
                else
                {
                    await CoreMethods.DisplayAlert("Erreur", "Il faut choisir un fichier", "Ok");
                }
            }
            else
            {
                await CoreMethods.DisplayAlert("Erreur", "Il faut être connecté à internet pour pouvoir ajouter un fichier", "Ok");
            }
        }
        
        );

        private async Task LoadProperties()
        {
            PropertyList = new ObservableCollection<ClientPropertyModel>();
            // Recherche Appartements
            var apartmentList = await StoreManager.ApartmentStore.GetItemsByContactId(ContactId);
            if (apartmentList != null && apartmentList.Any())
            {
                foreach (var item in apartmentList)
                {
                    var ClientProperty = new ClientPropertyModel
                    {
                        ResidenceName = item.ResidenceName,
                        PropertyType = ResidenceType.Appartement.ToString().ToLower(),
                        PropertyNumber = item.LotNumberArchitect,
                        ApartmentType = item.Kind,
                        Area = item.Area.ToString(),
                        CommandState = item.CommandState,
                        Price = item.Price,
                        Stage = item.Stage,
                        PropertyId = item.Id
                    };
                    PropertyList.Add(ClientProperty);
                }
            }

            // Recherche Cave
            var cellarList = await StoreManager.CellarStore.GetItemsByContactId(ContactId);
            if (cellarList != null && cellarList.Any())
            {
                foreach (var item in cellarList)
                {
                    var ClientProperty = new ClientPropertyModel
                    {
                        ResidenceName = item.ResidenceName,
                        PropertyType = ResidenceType.Cave.ToString().ToLower(),
                        PropertyNumber = item.LotNumberArchitect,
                        Area = item.Area.ToString(),
                        CommandState = item.CommandState,
                        Price = item.Price,
                        Stage = item.Stage,
                        PropertyId = item.Id
                    };
                    PropertyList.Add(ClientProperty);
                }
            }

            // Recherche Parking
            var garageList = await StoreManager.GarageStore.GetItemsByContactId(ContactId);
            if (garageList != null && garageList.Any())
            {
                foreach (var item in garageList)
                {
                    var ClientProperty = new ClientPropertyModel
                    {
                        ResidenceName = item.ResidenceName,
                        PropertyType = ResidenceType.Garage.ToString().ToLower(),
                        PropertyNumber = item.LotNumberArchitect,
                        Area = item.Area.ToString(),
                        CommandState = item.CommandState,
                        Price = item.Price,
                        Stage = item.Stage,
                        PropertyId = item.Id
                    };
                    PropertyList.Add(ClientProperty);
                }
            }
        }

            async void CalculReference(string residenceId, string typeBien)
        {
            if ((typeBien == null) || (residenceId == null)) return;

            if (typeBien == "Appartement")
            {
                var refs = await StoreManager.ApartmentStore.GetItemsByResidenceId(residenceId);
                if (refs != null && refs.Any())
                {
                    refs = refs.Where(x => string.IsNullOrEmpty(x.ContactId));
                    References = new ObservableCollection<string>();
                     
                    foreach (var item in refs)
                    {
                        References.Add(item.LotNumberArchitect);
                    }
                    ReferenceSelected = References[0];
                    CalculPrice(ReferenceSelected);
                }
            }
            if (typeBien == "Cave")
            {
                var refs = await StoreManager.CellarStore.GetItemsByResidenceId(residenceId);
                if (refs != null && refs.Any())
                {
                    refs = refs.Where(x => string.IsNullOrEmpty(x.ContactId));
                    References = new ObservableCollection<string>();
                    foreach (var item in refs)
                    {
                        References.Add(item.LotNumberArchitect);
                    }
                    ReferenceSelected = References[0];
                    CalculPrice(ReferenceSelected);
                }
            }
            if (typeBien == "Parking")
            {
                var refs = await StoreManager.GarageStore.GetItemsByResidenceId(residenceId);
                if (refs != null && refs.Any())
                {
                    refs = refs.Where(x => string.IsNullOrEmpty(x.ContactId));
                    References = new ObservableCollection<string>();
                    foreach (var item in refs)
                    {
                        References.Add(item.LotNumberArchitect);
                    }
                    ReferenceSelected = References[0];
                    CalculPrice(ReferenceSelected);
                }
            }
            
        }

        async void CalculPrice(string reference)
        {
            if (!String.IsNullOrEmpty(reference))
            {
                if (TypeBienSelected == "Appartement")
                {
                    var apt =await StoreManager.ApartmentStore.GetItemByRefenceAsync(reference);
                    Price = apt.Price;
                }
                if (TypeBienSelected == "Cave")
                {
                    var cellar = await StoreManager.CellarStore.GetItemByRefenceAsync(reference);
                    Price = cellar.Price;
                }
                if (TypeBienSelected == "Parking")
                {
                    var parking = await StoreManager.GarageStore.GetItemByRefenceAsync(reference);
                    Price = parking.Price;
                }


            }

        }

        public Command AddPropertyCommand => new Command(async (obj) =>
        {
            using (UserDialogs.Instance.Loading("Ajout du bien", null, null, true))
            {
                if (TypeBienSelected == "Appartement")
                {
                    var apt = await StoreManager.ApartmentStore.GetItemByRefenceAsync(ReferenceSelected);
                    if (apt != null)
                    {
                        apt.ContactId = ContactId;
                        await StoreManager.ApartmentStore.UpdateAsync(apt);
                    }
                }
                if (TypeBienSelected == "Cave")
                {
                    var cellar = await StoreManager.CellarStore.GetItemByRefenceAsync(ReferenceSelected);
                    if (cellar != null)
                    {
                        cellar.ContactId = ContactId;
                        await StoreManager.CellarStore.UpdateAsync(cellar);
                    }
                }
                if (TypeBienSelected == "Parking")
                {
                    var parking = await StoreManager.GarageStore.GetItemByRefenceAsync(ReferenceSelected);
                    if (parking != null)
                    {
                        parking.ContactId = ContactId;
                        await StoreManager.GarageStore.UpdateAsync(parking);
                    }
                }
                await LoadProperties();
            }
            TabThreeLevel--;
        });

        public Command LinkToPropertyCommand => new Command<ClientPropertyModel>(async (obj) =>
        {
            if (obj.PropertyType == ResidenceType.Appartement.ToString().ToLower())
            {
                Models.DataObjects.Apartment apt = await StoreManager.ApartmentStore.GetItemAsync(obj.PropertyId);

                RequestTabbedPage(apt);
            }

            if (obj.PropertyType == ResidenceType.Garage.ToString().ToLower())
            {
                Garage item = await StoreManager.GarageStore.GetItemAsync(obj.PropertyId);
                var garagesListItem = new GaragesListModel
                {
                    LotNumberArchitect = item.LotNumberArchitect,
                    Type = item.Type,
                    Area = item.Area,
                    Price = item.Price,
                    Id = item.Id,
                    ResidenceName = item.ResidenceName
                };
                await CoreMethods.PushPageModel<ViewModels.ResidencesDetailsGaragePageModel>(garagesListItem);
            }
            if (obj.PropertyType == ResidenceType.Cave.ToString().ToLower())
            {
                Cellar item = await StoreManager.CellarStore.GetItemAsync(obj.PropertyId);
                var cellarsListItem = new CellarsListModel
                {
                    LotNumberArchitect = item.LotNumberArchitect,
                    Area = item.Area,
                    Price = item.Price,
                    Id = item.Id,
                    ResidenceName = item.ResidenceName
                };
                await CoreMethods.PushPageModel<ViewModels.ResidencesDetailsCellarPageModel>(cellarsListItem);
            }

        });

        private async void RequestTabbedPage(Models.DataObjects.Apartment apt)
        {
            var title = apt.ResidenceName + " > " + "Appartement " + apt.LotNumberArchitect.ToString();

            var page_1 = FreshPageModelResolver.ResolvePageModel<ApartmentDetailInfoPageModel>(apt);
            page_1.Title = "Informations";

            var page_2 = FreshPageModelResolver.ResolvePageModel<ApartmentPlansPageModel>(apt);
            page_2.Title = "Plans";

            var tabbed_page = new TabbedPage() { Title = title };

            tabbed_page.Children.Add(page_1);
            tabbed_page.Children.Add(page_2);        

            await this.CurrentPage.Navigation.PushAsync(tabbed_page);
        }

    }
}
