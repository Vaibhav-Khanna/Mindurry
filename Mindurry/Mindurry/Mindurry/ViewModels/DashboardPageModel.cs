using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Microcharts;
using Mindurry.ViewModels.Base;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class DashboardPageModel : BasePageModel
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Test { get; set; }
        public DonutChart Chart1 { get; set; }
        public BarChart Chart2 { get; set; }
        public LineChart Chart3 { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
            EndDate = DateTime.Today;
            StartDate = DateTime.Today.AddMonths(-1);
            var entries = new Entry[]
            {
                new Microcharts.Entry(0.005f)
                {
                    Label ="Téléphone",
                    ValueLabel="5%",
                    Color = new SkiaSharp.SKColor(108,8,23),
                },
                new Microcharts.Entry(0.600f)
                {
                    Label ="Web",
                    ValueLabel="60%",
                    Color = new SkiaSharp.SKColor(159,24,44),
                },
                 new Microcharts.Entry(0.005f)
                {
                    Label ="Radio",
                    ValueLabel="5%",
                    Color = new SkiaSharp.SKColor(204,50,73),
                },
                  new Microcharts.Entry(0.10f)
                {
                    Label ="TV",
                    ValueLabel="10%",
                    Color = new SkiaSharp.SKColor(245,123,141),
                }
                ,
                new Microcharts.Entry(0.20f)
                {
                    Label ="Print",
                    ValueLabel="20%",
                    Color = new SkiaSharp.SKColor(245,202,208),
                }
                
            };
            Chart1 = new Microcharts.DonutChart { Entries = entries, HoleRadius= 0.50f };

            var entries2 = new Entry[]
{
                new Microcharts.Entry(1900)
                {
                    Label ="Herrian",
                    ValueLabel="1900",
                    Color = new SkiaSharp.SKColor(108,8,23),
                },
                new Microcharts.Entry(900)
                {
                    Label ="Herri Ondo",
                    ValueLabel="900",
                    Color = new SkiaSharp.SKColor(108,8,23),
                },
                new Microcharts.Entry(700)
                {
                    Label ="Migarri",
                    ValueLabel="700",
                    Color = new SkiaSharp.SKColor(108,8,23),
                }
                ,
                new Microcharts.Entry(2000)
                {
                    Label ="Villa Aguilera",
                    ValueLabel="2000",
                    Color = new SkiaSharp.SKColor(108,8,23),
                }
                ,
                new Microcharts.Entry(1200)
                {
                    Label ="Patio Choisy",
                    ValueLabel="1200",
                    Color = new SkiaSharp.SKColor(108,8,23),
                },
                new Microcharts.Entry(1600)
                {
                    Label ="Villa Nere Doya",
                    ValueLabel="1600",
                    Color = new SkiaSharp.SKColor(108,8,23),
                },
                new Microcharts.Entry(1600)
                {
                    Label ="Gaztelu",
                    ValueLabel="1600",
                    Color = new SkiaSharp.SKColor(108,8,23),
                }
};
            Chart2 = new Microcharts.BarChart { Entries = entries2 };

            var entries3 = new Entry[]
{
                new Microcharts.Entry(230)
                {
                    Label ="Jan",
                    ValueLabel="230",
                    Color = new SkiaSharp.SKColor(159,24,44),
                },
                new Microcharts.Entry(50)
                {
                    Label ="Fev",
                    ValueLabel="50",
                    Color = new SkiaSharp.SKColor(159,24,44),
                },
                new Microcharts.Entry(170)
                {
                    Label ="Mar",
                    ValueLabel="170",
                    Color = new SkiaSharp.SKColor(159,24,44),
                }
                ,
                new Microcharts.Entry(290)
                {
                    Label ="Avr",
                    ValueLabel="290",
                    Color = new SkiaSharp.SKColor(159,24,44),
                }
                ,
                new Microcharts.Entry(200)
                {
                    Label ="Mai",
                    ValueLabel="200",
                    Color = new SkiaSharp.SKColor(159,24,44),
                },
                new Microcharts.Entry(170)
                {
                    Label ="Juin",
                    ValueLabel="170",
                    Color = new SkiaSharp.SKColor(159,24,44)
                },
                 new Microcharts.Entry(250)
                {
                    Label ="Juil",
                    ValueLabel="250",
                    Color = new SkiaSharp.SKColor(159,24,44)
                }
};
            Chart3 = new Microcharts.LineChart { Entries = entries3 , LineMode= LineMode.Straight};
            Chart1.LabelTextSize = Chart2.LabelTextSize = 13;
        }

        
    }
}
