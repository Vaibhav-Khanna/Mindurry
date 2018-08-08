using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.DataModels
{
    public enum Statut { Libre, Reserve, Vendu, Option}
    public enum ResidenceType { Appartement, Garage, Cave }

    public class Residence
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
        public Statut Statut { get; set; }
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

        public List<Statut> Statuses { get; set; }
        public Statut SelectedStatus { get; set; }

        public Residence()
        {
            Statuses = new List<Statut> { Statut.Libre, Statut.Option, Statut.Reserve, Statut.Vendu };
        }
    }
}
