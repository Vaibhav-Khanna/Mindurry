using Mindurry.UWP.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportEffect(typeof(FontLightEffect), "FontLightEffect")]
namespace Mindurry.UWP.Effects
{
    public class FontLightEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control != null)
                ((Windows.UI.Xaml.Controls.TextBlock)Control).FontWeight = Windows.UI.Text.FontWeights.Light;
        }

        protected override void OnDetached()
        {
            
        }
    }
}
