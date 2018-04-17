using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    public class ConnexionPageModel : FreshBasePageModel
    {
        public ICommand LoadMainPageCommand { get; set; }

        public ConnexionPageModel()
        {
            LoadMainPageCommand = new Command(LoadMainPage);
        }

        void LoadMainPage()
        {
            CoreMethods.PopPageModel(true);
        }
    }
}
