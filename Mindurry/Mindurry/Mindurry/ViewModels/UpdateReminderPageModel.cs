using Mindurry.Models.DataObjects;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    class UpdateReminderPageModel : BasePageModel
    {
        private Note _note;
        public string Content { get; set; }
        public DateTimeOffset? ReminderDate { get; set; }

        public  override void Init(object initData)
        {
            base.Init(initData);
            _note = initData as Note;
            Content = _note.Content;
            ReminderDate = _note.ReminderAt;
        }

        public Command SaveReminderCommand => new Command(async (obj) =>
        {
            _note.Content = Content;
            _note.ReminderAt = ReminderDate;
            var updated = await StoreManager.NoteStore.UpdateAsync(_note);
            if (updated)
            {
                await CoreMethods.PopPageModel(_note, true, false);
            }
            else
            {
                await CoreMethods.DisplayAlert("Erreur", "Une erreur a eu lieu de l'enregistrement, merci de recommencer", "Ok");
            }

        });
    }
}
