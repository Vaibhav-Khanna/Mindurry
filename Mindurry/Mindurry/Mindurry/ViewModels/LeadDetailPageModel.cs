using FreshMvvm;
using Mindurry.DataModels;
using Mindurry.Models;
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
    public class LeadDetailPageModel : BasePageModel
    {
        private string ContactId;
        public Contact Contact { get; set; }

        public ObservableCollection<Note> Reminders { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public DateTimeOffset? DateReminder { get; set; } = null;
        public string TextNote { get; set; }

        public ObservableCollection<Activity> Activities { get; set; }

        public ObservableCollection<string> Combo1 { get; set; }
        public ObservableCollection<string> Types { get; set; }
        public ObservableCollection<string> Combo3 { get; set; }
        public ObservableCollection<string> Combo4 { get; set; }
        public string Combo1Selected { get; set; }
        public string TypeSelected { get; set; }
        public string Combo3Selected { get; set; }
        public string Combo4Selected { get; set; }
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

        public ObservableCollection<CheckBoxItem> ResidencesChecks { get; set; }
        public ObservableCollection<CheckBoxItem> TypesChecks { get; set; }

        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                var toto = isChecked;
            }
        }

        public async override void Init(object initData)
        {
            base.Init(initData);
            ContactId = (string)initData;

            await LoadContact();

            await LoadReminders();

            await LoadNotes();

            var activity1 = new Activity
            {
                Date = new DateTime(2018, 2, 21, 15, 5, 0),
                Name = "Manuel  Llop",
                Message = "Prise de contact effectué, rdv pris pour le vendredi 25",
                Icon = "",
                Color = Color.FromRgb(249, 249, 249),
                HorizontalOptions = Xamarin.Forms.LayoutOptions.End
            };

            var activity2 = new Activity
            {
                Date = new DateTime(2018, 2, 20, 15, 12, 0),
                Name = "John Doe",
                Message = "Bonjour, je souhaiterais en savoir plus sur le programme villa Aguiléra. Merci pour votre aide !",
                Icon = "",
                Color = Color.FromRgb(231, 243, 255),
                HorizontalOptions = Xamarin.Forms.LayoutOptions.Start
            };

            Activities = new ObservableCollection<Activity> { activity1, activity2 };

            Types = new ObservableCollection<string> { "Investisseur" };
            Combo1 = new ObservableCollection<string> { "Lead" };
            Combo3 = new ObservableCollection<string> { "Ne cherche plus" };

            Combo4 = new ObservableCollection<string> { "Arold Martino", "Jean Noosa" };

            Combo1Selected = Combo1[0];
            TypeSelected = Types[0];
            Combo3Selected = Combo3[0];
            Combo4Selected = Combo4[0];

            var item1 = new CheckBoxItem { Content = "Herrian" };
            var item2 = new CheckBoxItem { Content = "Herri Ondo", IsChecked = true };
            var item3 = new CheckBoxItem { Content = "Villa Aguiléra" };

            ResidencesChecks = new ObservableCollection<CheckBoxItem> { item1, item2, item3 };

            var item4 = new CheckBoxItem { Content = "Studio" };
            var item5 = new CheckBoxItem { Content = "T2", IsChecked = true };
            var item6 = new CheckBoxItem { Content = "T3" };

            TypesChecks = new ObservableCollection<CheckBoxItem> { item4, item5, item6 };

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
        

        public Command UpdateContactCommand => new Command(async (obj) =>
        {
            await CoreMethods.PushPageModel<UpdateReminderPageModel>(Contact.Id);
            SubUnsub();

        });

        
        public Command TerminateReminderCommand => new Command(async (obj) =>
        {
            
            var result = await CoreMethods.DisplayAlert("Classer", "Etes vous sur de vouloir classer ce rappel ?", "Oui", "Non");
            if (result)
            {
                Note _note = obj as Note;
                _note.DoneAt = DateTimeOffset.Now;
                _note.ActivityStreamDate = DateTimeOffset.Now;
                await StoreManager.NoteStore.UpdateAsync(_note);
                await LoadReminders();
            }

        });

        public Command UpdateReminderCommand => new Command(async (obj) =>
        {
            await CoreMethods.PushPageModel<UpdateReminderPageModel>(obj,true,false); 

        });
        public Command DeleteReminderCommand => new Command(async (obj) =>
        {
           var result = await CoreMethods.DisplayAlert("Suppression", "Etes vous sur de vouloir supprimer ce rappel ?", "Oui", "Non");
            if (result)
            {
                await StoreManager.NoteStore.RemoveAsync((Note)obj);
                await LoadReminders();
            }
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
    }
}
