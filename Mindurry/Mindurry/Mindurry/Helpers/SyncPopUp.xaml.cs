using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mindurry.Helpers
{
    public partial class SyncPopUp : PopupPage
    {
        bool ShouldAnimate = false;

        public SyncPopUp(string Text)
        {
            InitializeComponent();
            ToastLabel.Text = Text;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.StartTimer(new TimeSpan(0, 0, 5), HandleFunc);
        }

        bool HandleFunc()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (container != null)
                {
                    container.Padding = 0;
                    container.LayoutTo(new Rectangle(0, 0, container.Width, container.Height), 500, Easing.Linear);                 

                    ToastLabel.Text = "";
                    if (!ShouldAnimate)
                    {
                        ShouldAnimate = true;
                        Animate();
                    }
                }
            });

            return false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ShouldAnimate = false;
        }

        async void Animate()
        {
            here: await container?.FadeTo(0, 1000, Easing.Linear);
            await container?.FadeTo(1, 1000, Easing.Linear);

            if (ShouldAnimate)
                goto here;
        }

    }
}