using Mindurry.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    public class ConnexionPageModel : BasePageModel
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
