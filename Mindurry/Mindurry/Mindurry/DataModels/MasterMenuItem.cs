using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;

namespace Mindurry
{
    public class MasterMenuItem : INotifyPropertyChanged
    {
        string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }
        public string Icon { get; set; }

        public Type TagetType { get; set; } = typeof(ContentPage);

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class MasterMenuEventArgs : EventArgs
    {
        public MasterMenuItem item { get; internal set; }
        public MasterMenuEventArgs(MasterMenuItem _item)
        {
            this.item = _item;
        }
    }
}
