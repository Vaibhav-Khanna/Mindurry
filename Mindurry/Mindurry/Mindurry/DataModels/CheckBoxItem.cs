using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Mindurry.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CheckBoxItem : INotifyPropertyChanged
    {
        public bool IsChecked { get; set; }
        public string Content { get; set; }
        public string Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
