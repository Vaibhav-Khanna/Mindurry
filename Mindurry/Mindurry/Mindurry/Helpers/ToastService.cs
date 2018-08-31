using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mindurry.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Mindurry.Helpers
{
    public static class ToastService
    {

        public static async Task ShowSyncing(string text)
        {
            var popupPage = new SyncPopUp(text);

            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }

            await PopupNavigation.Instance.PushAsync(popupPage);
        }

        public static async Task Show(string text)
        {
            var popupPage = new ToastLayout(text, null);

            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }

            await PopupNavigation.Instance.PushAsync(popupPage);
        }

        public static async Task Show(string text, Color ToastColor)
        {
            var popupPage = new ToastLayout(text, ToastColor);

            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }

            await PopupNavigation.Instance.PushAsync(popupPage);
        }

        public static async Task Hide()
        {
            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }

    }
}
