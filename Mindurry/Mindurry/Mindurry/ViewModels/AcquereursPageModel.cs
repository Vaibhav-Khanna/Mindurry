using Mindurry.DataModels;
using Mindurry.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mindurry.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AcquereursPageModel : BasePageModel
    {
        public ObservableCollection<Acquereur> Acquereurs { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            var itema1 = new Acquereur
            {
                No = 123,
                Type = "T2",
                Client = "John Doe",
                Stade = "En attente des options"
            };

            var itema2 = new Acquereur
            {
                No = 123,
                Type = "T4",
                Client = "Marie Dupont",
                Stade = "Options validées"
            };

            var itema3 = new Acquereur
            {
                No = 123,
                Type = "T3",
                Client = "Albert Caro",
                Stade = "Options validées"
            };

            var itema4 = new Acquereur
            {
                No = 123,
                Type = "Studio",
                Client = "Claire Aront",
                Stade = "En attente des options"
            };

            Acquereurs = new ObservableCollection<Acquereur> { itema1, itema2, itema3, itema4 };
        }
    }
}
