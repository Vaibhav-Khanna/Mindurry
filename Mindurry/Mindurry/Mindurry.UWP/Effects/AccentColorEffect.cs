using Mindurry.UWP.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportEffect(typeof(AccentColorEffect), "AccentColorEffect")]
namespace Mindurry.UWP.Effects
{
    public class AccentColorEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control == null)
                return;
            if (Control is Windows.UI.Xaml.Controls.Button)
                ((Windows.UI.Xaml.Controls.Button)Control).Background = (Windows.UI.Xaml.Media.Brush)Control.Resources["SystemControlHighlightAccentBrush"];
            else if (Control is Windows.UI.Xaml.Controls.Control)
                ((Windows.UI.Xaml.Controls.Control)Control).Foreground = (Windows.UI.Xaml.Media.Brush) Control.Resources["SystemControlHighlightAccentBrush"];
            else if (Control is Windows.UI.Xaml.Controls.TextBlock)
                ((Windows.UI.Xaml.Controls.TextBlock)Control).Foreground = (Windows.UI.Xaml.Media.Brush)Control.Resources["SystemControlHighlightAccentBrush"];
        }

        protected override void OnDetached()
        {
            
        }
    }
}
