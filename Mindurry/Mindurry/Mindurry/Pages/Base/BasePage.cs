using System;
using Xamarin.Forms;
using FreshMvvm;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Mindurry.Pages.Base
{
    public class BasePage : FreshBaseContentPage
    {
        bool _islarge;
        public bool IsLargeTitles
        {
            get { return _islarge; }
            set
            {
                _islarge = value;

                if (value)
                    On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Always);
                else
                    On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);


            }
        }



    }
}
