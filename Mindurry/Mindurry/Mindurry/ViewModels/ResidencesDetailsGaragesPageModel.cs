using Mindurry.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidencesDetailsGaragesPageModel :FreshMvvm.FreshBasePageModel
    {
        bool IsGarage;

        public ObservableCollection<Residence> Garages { get; set; }
        public Residence SelectedGarage
        {
            get => null;
            set
            {
                if (value != null)
                    CoreMethods.PushPageModel<ViewModels.ResidencesDetailsGaragePageModel>(value);
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            IsGarage = (bool)initData;

            var itemg1 = new Residence
            {
                Parent = "John Doe",
                NoArchi = 2,
                Statut = Statut.Libre,
                Surface = 13,
                Prix = 10000,
                PlanFileName = "Plan-Version-Final06.pdf",
                NoCoPro = 5,
            };

            var itemg2 = new Residence
            {
                Parent = "John Doe",
                NoArchi = 23,
                Statut = Statut.Reserve,
                Surface = 9,
                Prix = 12000,
                PlanFileName = "Plan-Version-Final06.pdf",
                NoCoPro = 5,
            };

            var itemg3 = new Residence
            {
                Parent = "John Doe",
                NoArchi = 43,
                Statut = Statut.Vendu,
                Surface = 13,
                Prix = 11000,
                PlanFileName = "Plan-Version-Final06.pdf",
                NoCoPro = 5,
            };

            var itemg4 = new Residence
            {
                Parent = "John Doe",
                NoArchi = 23,
                Statut = Statut.Option,
                Surface = 13,
                Prix = 11000,
                PlanFileName = "Plan-Version-Final06.pdf",
                NoCoPro = 5,
            };

            Garages = new ObservableCollection<Residence> { itemg1, itemg2, itemg3, itemg4 };
        }
    }
}
