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

        public bool Chart1Visible { get; set; } = false;
        public bool Chart2Visible { get; set; } = false;
        public bool Chart3Visible { get; set; } = false;
        public bool Chart4Visible { get; set; } = false;
        public bool Chart5Visible { get; set; } = false;
        public bool Chart6Visible { get; set; } = false;

        private DateTime dateDeb { get; set; }
        private DateTime dateFin { get; set; }

        public DateTime DateDebDisplay { get; set; }
        public DateTime DateFinDisplay { get; set; }
        public DateTimeOffset MaxDate { get; set; }

       /* private DateTimeOffset? startDate;
        [PropertyChanged.DoNotNotify] */
        public DateTimeOffset? StartDate { get; set; }
        /* {
             get => startDate;
             set
             {
                 if (value != null)
                 {
                     dateDeb = value.Value.UtcDateTime.Date;                 
                     if (dateFin >= dateDeb)
                     {
                         DateDebDisplay = dateDeb;
                         startDate = value;
                         LoadCharts();
                         RaisePropertyChanged();
                     }
                     else
                     {
                         CoreMethods.DisplayAlert("Erreur", "La date de fin est inférieure à la date de début, Merci de corriger", "Ok");
                     }
                 }

             }
         } */

        /*  private DateTimeOffset? endDate;
        [PropertyChanged.DoNotNotify] */
        public DateTimeOffset? EndDate { get; set; }
       /* {
            get => endDate;
            set
            {
                if (value != null)
                {
                    dateFin = value.Value.UtcDateTime.AddDays(1).Date;
                    if (dateFin >= dateDeb)
                    {
                        DateFinDisplay = value.Value.UtcDateTime.Date;
                        endDate = value;
                        LoadCharts();

                        RaisePropertyChanged();
                    }
                    else
                    {
                        CoreMethods.DisplayAlert("Erreur", "La date de fin est inférieure à la date de début, Merci de corriger", "Ok");
                    }
                   
                }

            }
        }  */

        public DateTimeIntervalType IntervalType { get; set; }
        public double Interval { get; set; }

        public async override void Init(object initData)
        {
            base.Init(initData);
            MaxDate = DateTimeOffset.Now;
            EndDate = DateTimeOffset.Now;
            StartDate = DateTimeOffset.Now.AddMonths(-1);

            

            dateDeb = StartDate.Value.Date;
            dateFin = EndDate.Value.AddDays(1).Date;


            //await LoadCharts();

        }

        async Task LoadCharts()
        {
            //init Color charts
            List<SkiaSharp.SKColor> colorTab = new List<SkiaSharp.SKColor>();
            colorTab.Add(new SkiaSharp.SKColor(76, 175, 80));
            colorTab.Add(new SkiaSharp.SKColor(33, 150, 243));
            colorTab.Add(new SkiaSharp.SKColor(255, 235, 59));
            colorTab.Add(new SkiaSharp.SKColor(121, 85, 72));
            colorTab.Add(new SkiaSharp.SKColor(156, 39, 176));
            colorTab.Add(new SkiaSharp.SKColor(244, 67, 54));
            colorTab.Add(new SkiaSharp.SKColor(139, 195, 74));
            colorTab.Add(new SkiaSharp.SKColor(255, 152, 0));
            colorTab.Add(new SkiaSharp.SKColor(233, 30, 99));
            colorTab.Add(new SkiaSharp.SKColor(205, 220, 57));
            colorTab.Add(new SkiaSharp.SKColor(103, 58, 183));
            colorTab.Add(new SkiaSharp.SKColor(63, 81, 181));
            colorTab.Add(new SkiaSharp.SKColor(0, 150, 136));
            colorTab.Add(new SkiaSharp.SKColor(158, 158, 158));
            colorTab.Add(new SkiaSharp.SKColor(3, 169, 244));
            colorTab.Add(new SkiaSharp.SKColor(0, 188, 212));                 
            colorTab.Add(new SkiaSharp.SKColor(139, 195, 74)); 
            colorTab.Add(new SkiaSharp.SKColor(255, 193, 7));           
            colorTab.Add(new SkiaSharp.SKColor(255, 87, 34));           
            colorTab.Add(new SkiaSharp.SKColor(96, 125, 139));



            await Chart1Calcul(colorTab);

            await Chart2Calcul(colorTab);

            await Chart3Calcul();

            await Chart4Calcul();

            await Chart5Calcul(colorTab);

            await Chart6Calcul(colorTab);

                //----------------------------------------------------------------------------------------------------
                /* CustomBrushes = new List<Color>();
                 CustomBrushes.Add(Color.FromHex("6C0817"));
                 CustomBrushes.Add(Color.FromHex("9F1818"));
                 CustomBrushes.Add(Color.FromHex("CC3249"));
                 CustomBrushes.Add(Color.FromHex("F57B8D"));
                 CustomBrushes.Add(Color.FromHex("F5CAD0")); */

                //------------------------------------------------------------------------------------------------------------------------
        }
        //--------------------------------------------------------------------------------------

        
            public Command StartDateChangedCommand => new Command<DateTime>(async (startDate) =>
            {
                dateDeb = startDate.Date;
                if (dateFin >= dateDeb)
                {

                   // StartDate = startDate;
                    DateDebDisplay = startDate.Date;

                    await LoadCharts();
                }
                else
                {
                    await CoreMethods.DisplayAlert("Erreur", "La date de fin est inférieure à la date de début, Merci de corriger", "Ok");
                }

            });

        private  SkiaSharp.SKColor getRandomColor() {

            Random random = new Random();
            var redColor = (byte)random.Next(255);
            var greenColor = (byte)random.Next(255);
            var blueColor = (byte)random.Next(255);
            
            return new SkiaSharp.SKColor(redColor, greenColor, blueColor);
            }

            public Command EndDateChangedCommand => new Command<DateTime>(async (endDate) =>
            {
                dateFin = endDate.AddDays(1).Date;
                if (dateFin >= dateDeb)
                {
                   // EndDate = endDate.AddDays(1);
                    DateFinDisplay = endDate.Date;

                    await LoadCharts();
                }
                else
                {
                    await CoreMethods.DisplayAlert("Erreur", "La date de fin est inférieure à la date de début, Merci de corriger", "Ok");
                }

            });

        async Task Chart1Calcul(List<SkiaSharp.SKColor> colorTab)
        {
            
            //---------------------------------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------chart Interest By residence - Chart1----------------------------------------
            var residenceInterest = await StoreManager.ContactCustomFieldSourceEntryStore.GetItemsByContactCustomFieldSourceName("Résidences");
            if (residenceInterest != null && residenceInterest.Any())
            {
                Chart1Visible = true;
                var entries1 = new List<Microcharts.Entry>();
                var i = 0;
                foreach (var itemRes in residenceInterest)
                {

                    var contactInterested = await StoreManager.ContactCustomFieldStore.GetTotalCountByContactCustomFieldSourceEntryId(itemRes.Id, dateDeb, dateFin);

                    entries1.Add(
                        new Microcharts.Entry(contactInterested)
                        {
                            Label = itemRes.Value,
                            ValueLabel = contactInterested.ToString(),
                            //  Color = new SkiaSharp.SKColor(108, 8, 23),
                            Color = colorTab[i]
                        });
                    i++;
                }
                var listt = (entries1.OrderByDescending(x => x.Value)).ToList();
                var listEntries = new List<Microcharts.Entry>();

                // calcul limite boucle (max 5 elements)
                int limitFor;
                if (listt.Count < 5)
                {
                    limitFor = listt.Count;
                }
                else { limitFor = 5; }

                for (var r = 0; r < limitFor; r++)
                {
                    listEntries.Add(listt[r]);
                }

                Chart1 = new Microcharts.BarChart { Entries = listEntries };
            }
        }

        async Task Chart2Calcul(List<SkiaSharp.SKColor> colorTab)
        {
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
                                Color = colorTab[i],
                                
                            });
                }
                Chart2 = new Microcharts.DonutChart { Entries = entriesCollect, HoleRadius = 0.50f };
            }
        }

        async Task Chart3Calcul()
        {
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

        }

        async Task Chart4Calcul()
        {
            //------------------------------------------------------------------------------------------------------------------
            //---------------------------------------------------------------Chart 4 Acquereurs Pics---------------------------


            var clientPics = await StoreManager.NoteStore.GetContactStat(Qualification.Client, dateDeb, dateFin);
            if (clientPics != null && clientPics.Any())
            {
                //Parametrage de sfChart pour l'UI (interval type ...)
                UISfChartSettings();
                Chart4Visible = true;

                Chart4 = new ObservableCollection<PicsStats>(clientPics);


            }
        }

        async Task Chart5Calcul(List<SkiaSharp.SKColor> colorTab)
        {

            //-----------------------------------------------------------------------------------------------------------------
            //---------------------------------------------------------------Chart 5 Leads Sources---------------------------

            var leadsSources = await StoreManager.NoteStore.GetSourcesStat(Qualification.Lead, dateDeb, dateFin);

            if (leadsSources != null && leadsSources.Any())
            {
                //List de contact
                List<Contact> contactList = new List<Contact>();

                foreach (var itemS in leadsSources)
                {
                    var contact = await StoreManager.ContactStore.GetItemAsync(itemS.ContactId);
                    if (contact != null)
                    {
                        contactList.Add(contact);
                    }

                }
                long totalContact = contactList.Count();
                var tri = contactList.GroupBy(x => x.CollectSourceName)
                    .Select(g => new SourcesStats
                    {
                        Total = (g.Count() * 100) / totalContact,
                        SourceName = g.Key
                    })
                    .OrderBy(g => g.SourceName);




                if (tri != null && tri.Any())
                {
                    Chart5Visible = true;

                    var entriesLead = new List<Microcharts.Entry>();

                    var j = 0;
                    float percentLeadAutre = 0;
                    foreach (var item in tri)
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
                                    Color = colorTab[j],
                                    
                                });
                    }
                    Chart5 = new Microcharts.DonutChart { Entries = entriesLead, HoleRadius = 0.50f };
                }
            }
        }
        
        async Task Chart6Calcul(List<SkiaSharp.SKColor> colorTab)
        {
            //-------------------------------------------------------------------------------------------------------------------------------------------
            //---------------------------------------------Chart 5 Acquereurs sources
            //---------------------------------------------------------------Chart 5 Leads Sources---------------------------

            var clientsSources = await StoreManager.NoteStore.GetSourcesStat(Qualification.Client, dateDeb, dateFin);

            if (clientsSources != null && clientsSources.Any())
            {
                //List de contact
                List<Contact> contactList = new List<Contact>();

                foreach (var itemS in clientsSources)
                {
                    var contact = await StoreManager.ContactStore.GetItemAsync(itemS.ContactId);
                    if (contact != null)
                    {
                        contactList.Add(contact);
                    }

                }
                long totalContact = contactList.Count();
                var tri = contactList.GroupBy(x => x.CollectSourceName)
                    .Select(g => new SourcesStats
                    {
                        Total = (g.Count() * 100) / totalContact,
                        SourceName = g.Key
                    })
                    .OrderBy(g => g.SourceName);



                if (tri != null && tri.Any())
                {
                    Chart6Visible = true;
                    var entriesClient = new List<Microcharts.Entry>();

                    var k = 0;
                    float percentClientAutre = 0;
                    foreach (var item in tri)
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
                                    Color = colorTab[k],
                                   
                                });
                    }
                    Chart6 = new Microcharts.DonutChart { Entries = entriesClient, HoleRadius = 0.50f };
                }
            }
        } 

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
