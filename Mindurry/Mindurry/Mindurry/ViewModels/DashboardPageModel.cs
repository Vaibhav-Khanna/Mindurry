using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Microcharts;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class DashboardPageModel : FreshBasePageModel
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Test { get; set; }
        public DonutChart Chart1 { get; set; }
        public DonutChart Chart2 { get; set; }

        bool isLoggedIn;

        public override void Init(object initData)
        {
            base.Init(initData);
            EndDate = DateTime.Today;
            StartDate = DateTime.Today.AddMonths(-1);
            var entries = new Entry[]
            {
                new Microcharts.Entry(0.45f)
                {
                    Label ="Autres",
                    ValueLabel="45%",
                    Color = new SkiaSharp.SKColor(74,74,74),
                },
                new Microcharts.Entry(0.125f)
                {
                    Label ="Logicimmo.com",
                    ValueLabel="12.5%",
                    Color = new SkiaSharp.SKColor(74,144,255),
                }
                ,
                new Microcharts.Entry(0.125f)
                {
                    Label ="Immoneuf.com",
                    ValueLabel="12.5%",
                    Color = new SkiaSharp.SKColor(245,166,35),
                }
                ,
                new Microcharts.Entry(0.30f)
                {
                    Label ="Seloger.com",
                    ValueLabel="30%",
                    Color = new SkiaSharp.SKColor(126,211,33),
                }
            };
            Chart1 = new Microcharts.DonutChart { Entries = entries };

            var entries2 = new Entry[]
{
                new Microcharts.Entry(0.625f)
                {
                    Label ="Autres",
                    ValueLabel="62.5%",
                    Color = new SkiaSharp.SKColor(74,74,74),
                },
                new Microcharts.Entry(0.125f)
                {
                    Label ="Immoneuf.com",
                    ValueLabel="12.5%",
                    Color = new SkiaSharp.SKColor(245,166,35),
                }
                ,
                new Microcharts.Entry(0.25f)
                {
                    Label ="Seloger.com",
                    ValueLabel="25%",
                    Color = new SkiaSharp.SKColor(126,211,33),
                }
};
            Chart2 = new Microcharts.DonutChart { Entries = entries2 };
            Chart1.LabelTextSize = Chart2.LabelTextSize = 13;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (!isLoggedIn)
            {
                CoreMethods.PushPageModel<ConnexionPageModel>(null, true, false);
                isLoggedIn = true;
            }
        }
    }
}
