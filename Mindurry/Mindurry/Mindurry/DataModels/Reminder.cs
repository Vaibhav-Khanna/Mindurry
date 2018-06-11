using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.DataModels
{
    public class Reminder
    {
        public DateTime ReminderDate  { get; set; }
        public string ContactName { get; set; }
        public string Title { get; set; }
        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                ViewModels.StaticViewModel.OnSelectionChanged();
            }
        }
    }
}
