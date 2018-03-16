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
            ((Windows.UI.Xaml.Controls.Control)Control).Foreground = (Windows.UI.Xaml.Media.Brush) Control.Resources["SystemControlHighlightAccentBrush"];
        }

        protected override void OnDetached()
        {
            
        }
    }
}
