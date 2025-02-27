﻿using Acr.UserDialogs;
using FreshMvvm;
using Mindurry.DataModels;
using Mindurry.Helpers;
using Mindurry.Models;
using Mindurry.Models.DataObjects;
using Mindurry.Pages;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class LeadDetailPageModel : BasePageModel
    {
        private string ContactId;
        public Contact Contact { get; set; }

        public ObservableCollection<RemindersCheckBoxListModel> Reminders { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        private ObservableCollection<Note> OriginalNotes;
        public Boolean ButtonShowMoreIsDisplayed { get; set; }
        public Boolean ButtonShowLessIsDisplayed { get; set; } = false;
       // public DateTimeOffset? DateRem { get; set; }
        public DateTimeOffset MinDate { get; set; } = new DateTimeOffset(DateTimeOffset.Now.Date);
        public string TextNote { get; set; }

        public ObservableCollection<CollectSource> CollectSources { get; set; }

        DateTimeOffset? dateRem;
        [PropertyChanged.DoNotNotify]
        public DateTimeOffset? DateRem
        {
            get
            {
                return dateRem;
            }
            set
            {
                dateRem = value;
                RaisePropertyChanged();
            }
        }

        CollectSource selectedSource;
        [PropertyChanged.DoNotNotify]
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
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> Qualifications { get; set; }
        private string selectedQualification;
        [PropertyChanged.DoNotNotify]
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
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ContactCustomFieldSourceEntry> Types { get; set; }
       // public ContactCustomFieldSourceEntry SelectedType { get; set; }
        private ContactCustomFieldSourceEntry selectedType;
        [PropertyChanged.DoNotNotify]
        public ContactCustomFieldSourceEntry SelectedType
        {
            get
            {
                return selectedType;
            }
            set
            {
                if (value != selectedType && value != null && selectedType != null)
                {
                    SaveTypeContact(value.Id);
                }
                selectedType = value;
                RaisePropertyChanged();
            }
        }


        public ObservableCollection<CheckBoxItem> ResidencesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> TypesChecks { get; set; }
        public ObservableCollection<User> Commercials { get; set; }
        private User selectedCommercial;
        [PropertyChanged.DoNotNotify]
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
                RaisePropertyChanged();
            }
        }
        public string SequenceTitle { get; set; }

        public bool IsFirstListVisible { get; set; } = true;
        public bool IsSecondListVisible { get; set; } = true;
        public bool IsSwitchToggled { get; set; }
        public string ArrowTwo
        {
            get => IsSecondListVisible ? "" : "";
        }

        public ICommand ArrowOneCommand { get; set; }
        public ICommand ArrowTwoCommand { get; set; }
        public ICommand ClearAllResidencesCommand { get; set; }
        public ICommand ClearAllTypesCommand { get; set; }

        public bool IsReminders { get; set; }
        public bool IsSequence { get; set; }
        public bool IsTextNote { get; set; }

        public bool IsActivity { get; set; } = false;

        public async override void Init(object initData)
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
            if (string.IsNullOrEmpty(TextNote)) {
                IsTextNote = true; 
            }
            else {
                IsTextNote = false;
            }


            ArrowOneCommand = new Command(ChangeArrowOne);
            ArrowTwoCommand = new Command(ChangeArrowTwo);

            ClearAllResidencesCommand = new Command(ClearAllResidences);
            ClearAllTypesCommand = new Command(ClearAllTypes);
        }

        public async override void ReverseInit(object reverseData)
        {
            if (reverseData is Note)
            {
                await LoadReminders();
            }
        }

        protected  override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            PageWasPopped += LeadDetailPageModel_PageWasPopped;
            if ((PreviousPageModel is ContactsPageModel))
            {

            }
        }

        private void LeadDetailPageModel_PageWasPopped(object sender, EventArgs e)
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
                }
                IsReminders = true;
            }
            else
            {
                IsReminders = false;
            }
        }

        private async Task LoadNotes()
        {
            var notes = await StoreManager.NoteStore.GetNotesByContactIdAsync(ContactId);
            List<Note> notesFormat = new List<Note>();
            if (notes != null)
            {
                foreach (var item in notes)
                {

                    var textToTransform = item.Content;

                    item.Content = HtmlConvert.ConvertToPlainText(textToTransform);
                    notesFormat.Add(item);
                }
            }
           
            if (notesFormat != null && notesFormat.Any())
            {
                OriginalNotes = new ObservableCollection<Note>(notesFormat);
                if (OriginalNotes.Count > 3) { 
                Notes = new ObservableCollection<Note>(OriginalNotes.Take(3));
                    ButtonShowMoreIsDisplayed = true;
                }
                else
                {
                    Notes = new ObservableCollection<Note>(OriginalNotes);
                    ButtonShowMoreIsDisplayed = false;
                }
                IsActivity = true;
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
            foreach(var item in Qualifications)
            {
                if(item == Contact.Qualification)
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
            if (Contact.CollectSourceId != collectId)
            {
                    Contact.CollectSourceId = collectId;
                    await StoreManager.ContactStore.UpdateAsync(Contact);                 
            }
        }

        private async void SaveQualification(string qualification)
        {
            if (Contact.Qualification != qualification)
            {
                //using (UserDialogs.Instance.Loading("Sauvegarde du type de contact", null, null, true))
                //{
                    Contact.Qualification = qualification;
                    await StoreManager.ContactStore.UpdateAsync(Contact);
                //}
            }
            
        }
        private async void SaveTypeContact(string contactCustomEntryId)
        { 
            List<ContactCustomField> customField = (await StoreManager.ContactCustomFieldStore.GetItemsByContactCustomFieldSourceNameAndContactIdAsync("Type", ContactId)).ToList();
            if (customField != null && customField.Any())
            {
               // using (UserDialogs.Instance.Loading("Sauvegarde du type", null, null, true))
               // {
                    customField[0].ContactCustomFieldSourceEntryId = contactCustomEntryId;
                    await StoreManager.ContactCustomFieldStore.UpdateAsync(customField[0]);
               // }
            }          
        }

        public Command SequenceCommand => new Command(async () => { 

           await CoreMethods.PushPageModel<SequencePageModel>(ContactId, true);

        });

        public Command StopSequenceCommand => new Command(async () => {

            var sequence = await StoreManager.ContactSequenceStore.SequenceInProgress(ContactId);
            if (sequence != null)
            {
                sequence.EndAt = DateTimeOffset.Now;
                sequence.EndReason = "manualStop";
                sequence.ManualEndingUserId = Helpers.Settings.UserId;

                var result = await StoreManager.ContactSequenceStore.UpdateAsync(sequence);
                if (result)
                {
                    IsSequence = false;
                }
            }
            
        });


        public Command SaveChecksCommand => new Command<CheckBoxItem>(async (obj) =>
        {
            //using (UserDialogs.Instance.Loading("Sauvegarde/Suppression d'un intérêt", null, null, true))
           // {
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
                //Calcul contact customField field
                var customs = await StoreManager.ContactStore.RewriteCustomFields(ContactId);
                Contact.CustomFields = customs;

                //Update contact
                bool isInsertedContact = await StoreManager.ContactStore.UpdateAsync(Contact);
            //}

          //  MessagingCenter.Send(this, "ReloadCollection");
        });

        private async void SaveCommercial(string commercialId)
        {
            if (commercialId != Contact.UserId)
            {
                //using (UserDialogs.Instance.Loading("Sauvegarde du commercial", null, null, true))
               // {
                    Contact.UserId = commercialId;
                    await StoreManager.ContactStore.UpdateAsync(Contact);
                    //     MessagingCenter.Send(this, "ReloadCollection");
               // }
            }
                
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
            foreach(var item in ResidencesChecks.ToArray())
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

        public Command SendSequenceCommand => new Command(async (obj) =>
        {
            await CoreMethods.PushPageModel<NewContactPageModel>(Contact.Id);

        });

        public Command UpdateContactCommand => new Command(async (obj) =>
        {
            await CoreMethods.PushPageModel<NewContactPageModel>(Contact.Id);
            SubUnsub();

        });
       
        public Command SelectReminderCommand => new Command<RemindersCheckBoxListModel>(async (obj) =>
        {

            
            if (obj.IsChecked)
            {
                var result = await CoreMethods.DisplayAlert("Classer", "Etes vous sur de vouloir terminer ce rappel ?", "Oui", "Non");
                if (result)
                {
                    using (UserDialogs.Instance.Loading("Terminaison du rappel", null, null, true))
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
            await CoreMethods.PushPageModel<UpdateReminderPageModel>(obj.Reminder,true,false); 

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
            if (!string.IsNullOrEmpty(TextNote)) {

                 Note NoteToInsert = new Note()
                {
                    ContactId = ContactId,
                    UserId = Contact.UserId,
                    Kind = "note",
                    Content = TextNote
                };
                string title;

                if (DateRem != null)
                {

                    NoteToInsert.ReminderAt = DateRem;
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
                    if (DateRem != null)
                    {
                        await LoadReminders();
                        DateRem = null;
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

        public Command DisplayMoreNotesCommand => new Command( (obj) =>
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
            MessagingCenter.Subscribe<NewContactPageModel>(this, "ReloadDetailContact", async (obj) =>
            {

                await LoadContact();

                MessagingCenter.Unsubscribe<NewContactPageModel>(this, "ReloadDetailContact");
            });
        }
    }
}
