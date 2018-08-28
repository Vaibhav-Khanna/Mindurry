using Acr.UserDialogs;
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
        public DateTimeOffset MinDate { get; set; } = new DateTimeOffset(DateTimeOffset.Now.Date);

        public  override void Init(object initData)
        {
            base.Init(initData);
            _note = initData as Note;
            Content = _note.Content;
            ReminderDate = _note.ReminderAt;
        }

        public Command CloseCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel(true);


        });

        public Command SaveReminderCommand => new Command(async (obj) =>
        {
            bool updated;
            using (UserDialogs.Instance.Loading("Modification du rappel", null, null, true))
            {
                _note.Content = Content;
                _note.ReminderAt = ReminderDate;
                 updated = await StoreManager.NoteStore.UpdateAsync(_note);
            }
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
