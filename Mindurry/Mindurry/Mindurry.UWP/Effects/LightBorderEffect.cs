using Mindurry.UWP.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportEffect(typeof(LightBorderEffect), "LightBorderEffect")]
namespace Mindurry.UWP.Effects
{
    public class LightBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is Windows.UI.Xaml.Controls.TextBox)
                ((Windows.UI.Xaml.Controls.TextBox)Control).BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 122, 122, 122));
            else if (Control is Windows.UI.Xaml.Controls.ComboBox)
                ((Windows.UI.Xaml.Controls.ComboBox)Control).BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 122, 122, 122));
        }

        protected override void OnDetached()
        {
            
        }
    }
}
