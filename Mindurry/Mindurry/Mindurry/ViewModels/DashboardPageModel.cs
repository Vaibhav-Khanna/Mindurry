using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Microcharts;
using Mindurry.ViewModels.Base;
using Microsoft.WindowsAzure.MobileServices;
using Mindurry.Models.DataObjects;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Mindurry.DataStore.Implementation.Stores;
using System.Linq;
using Syncfusion.SfChart.XForms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class DashboardPageModel : BasePageModel
    {

        public string Test { get; set; }
        public DonutChart Chart1 { get; set; }
        public BarChart Chart2 { get; set; }
        public LineChart Chart3 { get; set; }

        public DateTime DateDeb { get; set; }
        public DateTime DateFin { get; set; }

        private DateTimeOffset? startDate;
        public DateTimeOffset? StartDate
        {
            get => startDate;
            set
            {
                if (value != null)
                {
                    DateDeb = value.Value.UtcDateTime.Date;
                    startDate = value;
                    LoadCharts();
                }

            }
        }

        private DateTimeOffset? endDate;
        public DateTimeOffset? EndDate
        {
            get => endDate;
            set
            {
                if (value != null)
                {
                    DateFin = value.Value.UtcDateTime.Date;
                    endDate = value;
                    LoadCharts();
                }

            }
        }
        public ObservableCollection<StatForm> DataLead { get; set;}
        public ObservableCollection<StatForm> DataClient { get; set; }

        public bool ChartLeadVisible { get; set; }
        public bool ChartClientVisible { get; set; }

        public DateTimeIntervalType IntervalType { get; set; }
        public double Interval { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
            EndDate = DateTimeOffset.Now;
            StartDate = DateTimeOffset.Now.AddMonths(-1);


        }

        public async Task LoadCharts()
        {
            // collectSource general
            var entriesCollect = new List<Entry>();
            //liste des collectSource
            var collectSources = await StoreManager.CollectSourceStore.GetItemsAsync();
            //nbre total de contact
            var contacts = await StoreManager.ContactStore.GetItemsAsync();
            long totalContacts = (contacts as IQueryResultEnumerable<Contact>).TotalCount;
            List<SkiaSharp.SKColor> colorTab = new List<SkiaSharp.SKColor>();
            colorTab.Add(new SkiaSharp.SKColor(108, 8, 23));
            colorTab.Add(new SkiaSharp.SKColor(159, 24, 44));
            colorTab.Add(new SkiaSharp.SKColor(204, 50, 73));
            colorTab.Add(new SkiaSharp.SKColor(245, 123, 141));
            colorTab.Add(new SkiaSharp.SKColor(245, 202, 208));
            var i = 0;
            float percentAutre = 0;
            foreach (var cs in collectSources)
            {
                long total = await StoreManager.ContactStore.GetTotalCountByCollectSourceId(cs.Id);
                if (total > 0)
                {
                    float percent = (total * 100) / totalContacts;
                    if (percent < 5)
                    {
                        percentAutre += percent;
                    }
                    else
                    {
                        entriesCollect.Add(
                            new Microcharts.Entry(percent)
                            {
                                Label = cs.Name,
                                ValueLabel = percent + "%",
                                Color = colorTab[i],
                            });
                        i++;
                    }
                }

            }
            if (percentAutre > 0)
            {
                entriesCollect.Add(
                        new Microcharts.Entry(percentAutre)
                        {
                            Label = "Autres",
                            ValueLabel = percentAutre + "%",
                            Color = new SkiaSharp.SKColor(204, 202, 208)
                        });
            }
            /*
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
                
            }; */
            Chart1 = new Microcharts.DonutChart { Entries = entriesCollect, HoleRadius = 0.50f };

            //chart Interest By residence
            var residenceInterest = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Résidences");
            var entries2 = new List<Entry>();

            foreach (var itemRes in residenceInterest)
            {

                var contactInterested = await StoreManager.ContactCustomFieldStore.GetTotalCountByContactCustomFieldSourceEntryId(itemRes.Id, DateDeb, DateFin);
                entries2.Add(
                    new Microcharts.Entry(contactInterested)
                    {
                        Label = itemRes.Value,
                        ValueLabel = contactInterested.ToString(),
                        Color = new SkiaSharp.SKColor(108, 8, 23),
                    });

            }
            Chart2 = new Microcharts.BarChart { Entries = entries2 };
            //--------------------------------------------------------------------------------
            // Chart 3 for lead

            var leadPics = await StoreManager.NoteStore.GetContactStat(Qualification.Lead, DateDeb, DateFin);
            if (leadPics != null && leadPics.Any())
            {
                ChartLeadVisible = true;
                DataLead = new ObservableCollection<StatForm>(leadPics);
            }
            else
            {
                DataLead = new ObservableCollection<StatForm>();
                ChartLeadVisible = false;
            }

            //--------------------------------------------------------------------------------
            // Chart 4 for lead


            var clientPics = await StoreManager.NoteStore.GetContactStat(Qualification.Client, DateDeb, DateFin);
            if (clientPics != null && leadPics.Any())
            {
                ChartClientVisible = true;
                DataClient = new ObservableCollection<StatForm>(clientPics);
            }
            else
            {
                DataClient = new ObservableCollection<StatForm>();
                ChartClientVisible = false;
            }

            //Parametrage de sfChart pour l'UI (interval type ...)
            UISfChartSettings();
//-----------------------------------------------------------------------------------------------------------------



            Chart1.LabelTextSize = Chart2.LabelTextSize = 13;

        }
        private void UISfChartSettings()
        {
                TimeSpan duration = DateFin - DateDeb;
                if (duration.TotalDays< 8 )
            {
                //Chart by day
                IntervalType = DateTimeIntervalType.Days;
                Interval = 1;
                
            }
                else if (duration.TotalDays< 32)
            {
                //chart by week
                IntervalType = DateTimeIntervalType.Days;
                Interval = 3;
            }
                else if ((duration.TotalDays< 365))
            {
                IntervalType = DateTimeIntervalType.Months;
                Interval = 1;
            }
                else
            {
                IntervalType = DateTimeIntervalType.Years;
                Interval = 1;
            }
        }
    }
}
