using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Mindurry.Helpers
{
    public partial class ToastLayout : PopupPage
    {


        public ToastLayout(string text, Color? color)
        {
            InitializeComponent();
            ToastLabel.Text = text;

            if (color != null)
            {
                container.BackgroundColor = (Color)color;
            }
        }

        protected override void OnAppearingAnimationEnd()
        {
            Device.StartTimer(new TimeSpan(0, 0, 2), () =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAllAsync();
                });

                return false;
            });

            base.OnAppearingAnimationEnd();
        }

    }
}
