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
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class RemindersPageModel : BasePageModel
    {

        public ObservableCollection<RemindersCheckBoxListModel> Reminders { get; set; }

        public long RemindersToDoCount { get; set; }
        public long RemindersDoneCount { get; set; }
        public bool IsToDo { get; set; }
        public Color BGDoneColor { get; set; }
        public Color BGToDoColor { get; set; }

        public async override void Init(object initData)
        {
            base.Init(initData);

            IsToDo = true;
            await LoadReminders("ToDo");         
            BGToDoColor = Color.Black;
            BGDoneColor = Color.Gray;
        }

        public async override void ReverseInit(object reverseData)
        {
            if (reverseData is Note)
            {
                Note Note = (Note)reverseData;
                string action;
                if (Note.DoneAt != null)
                {
                    action = "Done";
                }
                else
                {
                    action = "ToDo";
                }
                await LoadReminders(action);
            }
        }

        private async Task LoadReminders(string action)
        {
            Reminders = new ObservableCollection<RemindersCheckBoxListModel>();
            IEnumerable<Note> reminders = null;
            IEnumerable<Note> remindersToDo;
            IEnumerable<Note> remindersDone;
            // ToDO
            remindersToDo = await StoreManager.NoteStore.GetRemindersToDoAsync();
            RemindersToDoCount = remindersToDo.Count();
            // Done
            remindersDone = await StoreManager.NoteStore.GetRemindersDoneAsync();
            RemindersDoneCount = remindersDone.Count();
            if (action == "ToDo")
            {
                reminders = remindersToDo;
                BGDoneColor = Color.Gray;
                BGToDoColor = Color.Black;
            }
            else
            {
                reminders = remindersDone;
                BGDoneColor = Color.Black;
                BGToDoColor = Color.Gray;

            }
           
            
            if (reminders != null && reminders.Any())
            {
                foreach (var item in reminders)
                {
                    var reminderList = new RemindersCheckBoxListModel
                    {
                        Reminder = item,
                        IsChecked = false,
                        IsVisible = IsToDo
                    };
                    Reminders.Add(reminderList);
                }
            }
        }
        public Command RemindersToDoCommand => new Command( async() =>
        {
            IsToDo = true;
            await LoadReminders("ToDo");
                
        });
        public Command RemindersDoneCommand => new Command( async () =>
        {
            IsToDo = false;
            await LoadReminders("Done");
           
        });
        public Command SelectReminderCommand => new Command<RemindersCheckBoxListModel>(async (obj) =>
        {
            if (obj.IsChecked)
            {
                var result = await CoreMethods.DisplayAlert("Classer", "Etes vous sur de vouloir terminer ce rappel ?", "Oui", "Non");
                if (result)
                {
                    RemindersCheckBoxListModel reminderObj = obj as RemindersCheckBoxListModel;
                    Note _note = reminderObj.Reminder;
                    _note.DoneAt = DateTimeOffset.Now;
                    _note.ActivityStreamDate = DateTimeOffset.Now;
                    await StoreManager.NoteStore.UpdateAsync(_note);
                    await LoadReminders("Done");
                    
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
                await StoreManager.NoteStore.RemoveAsync((Note)obj.Reminder);
                string action;
                if (obj.Reminder.DoneAt != null)
                {
                    action = "Done";
                }
                else
                {
                    action = "ToDo";
                }
                await LoadReminders(action);
            }
        });
    }
}