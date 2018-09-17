using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Mindurry.Models.DataObjects;
using Mindurry.ViewModels;

namespace Mindurry.DataModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CheckBoxItem : INotifyPropertyChanged
    {
        bool _checked;
        public bool IsChecked
        {
            get { return _checked; }
            set
            {
                var temp = _checked;

                _checked = value;

                if (temp != value)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsChecked")); 
            }
        }

        public CheckBoxContainerDataType DataType { get; set; }
        public string Content { get; set; }
        public string Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public enum CheckBoxContainerDataType
    {
        Residence, Type, Exposure, CommandState
    }
}
