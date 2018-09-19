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
using Xamarin.Forms;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class DashboardPageModel : BasePageModel
    {

        public BarChart Chart1 { get; set; }
        public DonutChart Chart2 { get; set; }
        public ObservableCollection<PicsStats> Chart3 { get; set; }
        public ObservableCollection<PicsStats> Chart4 { get; set; }
        public DonutChart Chart5 { get; set; }
        public DonutChart Chart6 { get; set; }



        private DateTime dateDeb { get; set; }
        private DateTime dateFin { get; set; }

        public DateTime DateDebDisplay { get; set; }
        public DateTime DateFinDisplay { get; set; }
        public DateTimeOffset MaxDate { get; set; }

        private DateTimeOffset? startDate;
        [PropertyChanged.DoNotNotify]
        public DateTimeOffset? StartDate
        {
            get => startDate;
            set
            {
                if (value != null)
                {
                    dateDeb = value.Value.UtcDateTime.Date;
                    DateDebDisplay = dateDeb;
                    startDate = value;
                    LoadCharts();
                    RaisePropertyChanged();
                }

            }
        }

        private DateTimeOffset? endDate;
        [PropertyChanged.DoNotNotify]
        public DateTimeOffset? EndDate
        {
            get => endDate;
            set
            {
                if (value != null)
                {
                    dateFin = value.Value.UtcDateTime.AddDays(1).Date;
                    DateFinDisplay = value.Value.UtcDateTime.Date;
                    endDate = value;
                    LoadCharts();
                    RaisePropertyChanged();
                }

            }
        }

        public DateTimeIntervalType IntervalType { get; set; }
        public double Interval { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
            MaxDate = DateTimeOffset.Now;
            EndDate = DateTimeOffset.Now;
            StartDate = DateTimeOffset.Now.AddMonths(-1);
         
        }

        public bool Chart1Visible { get; set; } = false;
        public bool Chart2Visible { get; set; } = false;
        public bool Chart3Visible { get; set; } = false;
        public bool Chart4Visible { get; set; } = false;
        public bool Chart5Visible { get; set; } = false;
        public bool Chart6Visible { get; set; } = false;

        public async Task LoadCharts()
        {
            //Verif si 2 dates incoiherente
            if (dateDeb > dateFin)
            {
                await CoreMethods.DisplayAlert("Erreur", "La date de fin est inférieure à la date de début, Merci de corriger", "Ok");

            }
            else
            {

            
            //init Color charts
            List<SkiaSharp.SKColor> colorTab = new List<SkiaSharp.SKColor>();
            colorTab.Add(new SkiaSharp.SKColor(108, 8, 23));
            colorTab.Add(new SkiaSharp.SKColor(159, 24, 44));
            colorTab.Add(new SkiaSharp.SKColor(204, 50, 73));
            colorTab.Add(new SkiaSharp.SKColor(245, 123, 141));
            colorTab.Add(new SkiaSharp.SKColor(245, 202, 208));
            //---------------------------------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------chart Interest By residence - Chart1----------------------------------------
            var residenceInterest = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Résidences");
            if (residenceInterest != null && residenceInterest.Any())
            {
                Chart1Visible = true;
                var entries1 = new List<Microcharts.Entry>();

                foreach (var itemRes in residenceInterest)
                {

                    var contactInterested = await StoreManager.ContactCustomFieldStore.GetTotalCountByContactCustomFieldSourceEntryId(itemRes.Id, dateDeb, dateFin);
                    entries1.Add(
                        new Microcharts.Entry(contactInterested)
                        {
                            Label = itemRes.Value,
                            ValueLabel = contactInterested.ToString(),
                            Color = new SkiaSharp.SKColor(108, 8, 23),
                        });

                }
                Chart1 = new Microcharts.BarChart { Entries = entries1 };
            }
//----------------------------------------------------------------------------------------------------------------------
//--------------------------Chart Sources d'acquisitions - Chart2---------------------------------------------------------------- 

            
            //liste des collectSource
            var collectSources = await StoreManager.CollectSourceStore.GetItemsAsync();
            if (collectSources != null && collectSources.Any())
            {
                Chart2Visible = true;
                // collectSource general
                var entriesCollect = new List<Microcharts.Entry>();

                //nbre total de contact
                var contacts = await StoreManager.ContactStore.GetItemsAsync();
                long totalContacts = (contacts as IQueryResultEnumerable<Contact>).TotalCount;
                
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
                Chart2 = new Microcharts.DonutChart { Entries = entriesCollect, HoleRadius = 0.50f };
            }

//---------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------Chart 3 Leads Pics------------------------------------------------------

            var leadPics = await StoreManager.NoteStore.GetContactStat(Qualification.Lead, dateDeb, dateFin);
            if (leadPics != null && leadPics.Any())
            {
                //Parametrage de sfChart pour l'UI (interval type ...)
                UISfChartSettings();

                Chart3Visible = true;
                Chart3 = new ObservableCollection<PicsStats>(leadPics);

                
            }
           

//------------------------------------------------------------------------------------------------------------------
//---------------------------------------------------------------Chart 4 Acquereurs Pics---------------------------


            var clientPics = await StoreManager.NoteStore.GetContactStat(Qualification.Client, dateDeb, dateFin);
            if (clientPics != null && leadPics.Any())
            {
                //Parametrage de sfChart pour l'UI (interval type ...)
                UISfChartSettings();
                Chart4Visible = true;

                Chart4 = new ObservableCollection<PicsStats>(clientPics);

                
            }
           

            
            //-----------------------------------------------------------------------------------------------------------------
            //---------------------------------------------------------------Chart 5 Leads Sources---------------------------
           
            var leadsSources = await StoreManager.NoteStore.GetSourcesStat(Qualification.Lead, dateDeb, dateFin);
            if (leadsSources != null && leadsSources.Any())
            {
                Chart5Visible = true;

                var entriesLead = new List<Microcharts.Entry>();

                var j = 0;
                float percentLeadAutre = 0;
                foreach (var item in leadsSources)
                {

                    if (item.Total > 0)
                    {

                        if (item.Total < 5)
                        {
                            percentLeadAutre += item.Total;
                        }
                        else
                        {
                            entriesLead.Add(
                                new Microcharts.Entry(item.Total)
                                {
                                    Label = item.SourceName,
                                    ValueLabel = item.Total + "%",
                                    Color = colorTab[j],
                                });
                            j++;
                        }
                    }
                }
                if (percentLeadAutre > 0)
                {
                    entriesLead.Add(
                            new Microcharts.Entry(percentLeadAutre)
                            {
                                Label = "Autres",
                                ValueLabel = percentLeadAutre + "%",
                                Color = new SkiaSharp.SKColor(204, 202, 208)
                            });
                }
                Chart5 = new Microcharts.DonutChart { Entries = entriesLead, HoleRadius = 0.50f };
            }
                //-------------------------------------------------------------------------------------------------------------------------------------------
                //---------------------------------------------Chart 6 Acquereurs sources
                //---------------------------------------------------------------Chart 5 Leads Sources---------------------------
                
                var clientsSources = await StoreManager.NoteStore.GetSourcesStat(Qualification.Client, dateDeb, dateFin);
                if (clientsSources != null && clientsSources.Any())
                {
                Chart6Visible = true;
                var entriesClient = new List<Microcharts.Entry>();

                var k = 0;
                    float percentClientAutre = 0;
                    foreach (var item in clientsSources)
                    {

                        if (item.Total > 0)
                        {

                            if (item.Total < 5)
                            {
                                percentClientAutre += item.Total;
                            }
                            else
                            {
                                entriesClient.Add(
                                    new Microcharts.Entry(item.Total)
                                    {
                                        Label = item.SourceName,
                                        ValueLabel = item.Total + "%",
                                        Color = colorTab[k],
                                    });
                                k++;
                            }
                        }
                    }
                    if (percentClientAutre > 0)
                    {
                        entriesClient.Add(
                                new Microcharts.Entry(percentClientAutre)
                                {
                                    Label = "Autres",
                                    ValueLabel = percentClientAutre + "%",
                                    Color = new SkiaSharp.SKColor(204, 202, 208)
                                });
                    }
                    Chart6 = new Microcharts.DonutChart { Entries = entriesClient, HoleRadius = 0.50f };
                }



                //----------------------------------------------------------------------------------------------------
                /* CustomBrushes = new List<Color>();
                 CustomBrushes.Add(Color.FromHex("6C0817"));
                 CustomBrushes.Add(Color.FromHex("9F1818"));
                 CustomBrushes.Add(Color.FromHex("CC3249"));
                 CustomBrushes.Add(Color.FromHex("F57B8D"));
                 CustomBrushes.Add(Color.FromHex("F5CAD0")); */

                //------------------------------------------------------------------------------------------------------------------------

            }
        }
//--------------------------------------------------------------------------------------






        private void UISfChartSettings()
        {
                TimeSpan duration = dateFin - dateDeb;
                if (duration.TotalDays< 15)
            {
                //Chart by day
                IntervalType = DateTimeIntervalType.Days;
                Interval = 1;
                
            }
                else if (duration.TotalDays< 45)
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
