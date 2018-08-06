﻿using FreshMvvm;
using Mindurry.DataModels;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ClientsDetailsInfoPageModel : BasePageModel
    {
        private string ContactId;
        public Contact Contact { get; set; }

        public ObservableCollection<Note> Reminders { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public DateTimeOffset? DateReminder { get; set; } = null;
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

        public ObservableCollection<string> Combo1 { get; set; }
        public ObservableCollection<string> ComboL1 { get; set; }
        public ObservableCollection<string> ComboL2 { get; set; }
        public ObservableCollection<string> ComboL3 { get; set; }

        public ObservableCollection<DateTitle> Documents { get; set; }
        public ObservableCollection<DataModels.Residence> Residences { get; set; }

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
        public bool IsTabThreeL3 => TabThreeLevel == 2;

        public string Combo1Selected { get; set; }
        public string ComboL1Selected { get; set; }
        public string ComboL2Selected { get; set; }
        public string ComboL3Selected { get; set; }
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

        public ObservableCollection<string> Combo4 { get; set; }
        public string Combo4Selected { get; set; }

        public override async void Init(object initData)
        {
            base.Init(initData);
            ContactId = (string)initData;

            await LoadContact();

            await LoadReminders();

            await LoadNotes();

            await LoadFilters();

            var doc1 = new DateTitle
            {
                Date = new DateTime(2018, 2, 12),
                Title = "Compromis de vente"
            };

            var doc2 = new DateTitle
            {
                Date = new DateTime(2018, 1, 10),
                Title = "Plan séjour"
            };

            var doc3 = new DateTitle
            {
                Date = new DateTime(2018, 1, 8),
                Title = "Plan global"
            };

            var doc4 = new DateTitle
            {
                Date = new DateTime(2018, 12, 12),
                Title = "Contrat"
            };

            Documents = new ObservableCollection<DateTitle> { doc1, doc2, doc3, doc4 };

            var residence1 = new DataModels.Residence()
            {
                NoArchi = 456,
                Type = "T3",
                Surface = 86,
                Prix = 126000,
                Stade = "En attente des options",
                ResidenceType = ResidenceType.Apartment,            
                SelectedStatus = Statut.Reserve
            };

            Residences = new ObservableCollection<DataModels.Residence> { residence1 };

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

        private async Task LoadContact()
        {
            Contact = await StoreManager.ContactStore.GetItemAsync(ContactId);
        }

        private async Task LoadReminders()
        {
            var reminders = await StoreManager.NoteStore.GetNextRemindersByContactIdAsync(ContactId);
            if (reminders != null && reminders.Any())
            {
                Reminders = new ObservableCollection<Note>(reminders);
            }
            else
            {
                Reminders = new ObservableCollection<Note>();
            }
        }

        private async Task LoadNotes()
        {
            var notes = await StoreManager.NoteStore.GetNotesByContactIdAsync(ContactId);
            if (notes != null && notes.Any())
            {
                Notes = new ObservableCollection<Note>(notes);
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
            await CoreMethods.PushPageModel<NewContactPageModel>(Contact.Id);
            SubUnsub();

        });

        public Command AddNoteCommand => new Command(async (obj) =>
        {
            Note NoteToInsert = new Note()
            {
                ContactId = ContactId,
                UserId = Contact.UserId,
                Kind = "note",
                Content = TextNote
            };
            if (DateReminder != null) { NoteToInsert.ReminderAt = DateReminder; }
            await StoreManager.NoteStore.InsertAsync(NoteToInsert);
            await LoadNotes();

        });


        void SubUnsub()
        {
            MessagingCenter.Subscribe<NewContactPageModel>(this, "ReloadDetailContact", async (obj) =>
            {

                await LoadContact();

                MessagingCenter.Unsubscribe<NewContactPageModel>(this, "ReloadDetailContact");
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

        void TabTwo()
        {
            TabIndex = 1;
        }

        void TabThree()
        {
            TabIndex = 2;
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
    }
}
