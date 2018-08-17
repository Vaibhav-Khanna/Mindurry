﻿using Mindurry.DataModels;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ResidenceDetailsCellarsPageModel : BasePageModel
    {
        bool IsGarage;

        public ObservableCollection<ResidenceModel> Garages { get; set; }
        public ResidenceModel SelectedGarage
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

            var data = (Tuple<bool, string>)initData;
            IsGarage = data.Item1;

            //var itemg1 = new ResidenceModel
            //{
            //    Parent = "John Doe",
            //    NoArchi = 2,               
            //    Surface = 13,
            //    Price = 10000,
            //    PlanFileName = "Plan-Version-Final06.pdf"
            //};

            //var itemg2 = new ResidenceModel
            //{
            //    Parent = "Isabelle Soliu",
            //    NoArchi = 2,
            //    Surface = 12,
            //    Price = 5000,
            //    PlanFileName = "Plan-Version-Final06.pdf"

            //};

            //var itemg3 = new ResidenceModel
            //{
            //    Parent = "Frank Rico",
            //    NoArchi = 3,
            //    Surface = 12,
            //    Price = 5000,
            //    PlanFileName = "Plan-Version-Final06.pdf"

            //};

            //var itemg4 = new ResidenceModel
            //{
            //    Parent = "Aron Yoto",
            //    NoArchi = 4,
            //    Surface = 13,
            //    Price = 11000,
            //    PlanFileName = "Plan-Version-Final06.pdf"

            //};

            Garages = new ObservableCollection<ResidenceModel> {  };
        }
    }
}
