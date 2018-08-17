using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.DataModels
{
    class Apartment
    {
        public string Parent { get; set; }
        public ResidenceType ResidenceType { get; set; }
        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                ViewModels.StaticViewModel.OnSelectionChanged();
            }
        }
        public int NoArchi { get; set; }
        public string Type { get; set; }
        public Status Statut { get; set; }
        public string Exposition { get; set; }
        public double Surface { get; set; }
        public double Terasse { get; set; }
        public double Jardin { get; set; }
        public double Etage { get; set; }
        public double Prix { get; set; }
        public string Client { get; set; }
        public string Stade { get; set; }
        public int NoCoPro { get; set; }

        public string PlanFileName { get; set; }
        public List<string> Terasses { get; set; }
        public string JardinSurface { get; set; }

        public List<Status> Statuses { get; set; }
        public Status SelectedStatus { get; set; }

        public Apartment()
        {
            Statuses = new List<Status> { Status.Libre, Status.Option, Status.Reserve, Status.Vendu };
        }
    }
}

