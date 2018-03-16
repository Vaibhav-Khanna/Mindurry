using Mindurry.UWP.Effects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(SemiBoldEffect), "SemiBoldEffect")]
namespace Mindurry.UWP.Effects
{
    public class SemiBoldEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control != null)
                ((Windows.UI.Xaml.Controls.TextBlock)Control).FontWeight = Windows.UI.Text.FontWeights.SemiBold;
        }

        protected override void OnDetached()
        {
            
        }
    }
}
