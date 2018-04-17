using Mindurry.UWP.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(MasterRenderer))]
namespace Mindurry.UWP.Renderers
{
    public class MasterRenderer : Xamarin.Forms.Platform.UWP.MasterDetailPageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MasterDetailPage> e)
        {
            base.OnElementChanged(e);

            Control.CollapseStyle = Xamarin.Forms.PlatformConfiguration.WindowsSpecific.CollapseStyle.Partial;
        }
    }
}
