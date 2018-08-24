using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mindurry.Models
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class RemindersCheckBoxListModel : INotifyPropertyChanged
    {
        public Note Reminder { get; set; }
        public bool IsChecked { get; set; }
        public bool IsVisible { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
