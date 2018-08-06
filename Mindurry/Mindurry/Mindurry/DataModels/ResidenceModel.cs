using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.DataModels
{
    public enum Status { Libre, Reserve, Vendu, Option}
    public enum ResidenceType { Apartment, Garage, Cave }

    public class ResidenceModel
    {
        public Residence Residence { get; set; }
        public Models.DataObjects.Apartment Apartment { get; set; }
        public Cellar Cellar { get; set; }
        public Terrace Terrace { get; set; }

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
        public Status Status { get; set; }
        public string Exposition { get; set; }
        public double Surface { get; set; }
        public double Terace { get; set; }
        public double Garden { get; set; }
        public double Etage { get; set; }
        public double Price { get; set; }
        public string Client { get; set; }
        public string Stade { get; set; }
        public int NoCoPro { get; set; }
       
        public string PlanFileName { get; set; }
        public List<string> Terraces { get; set; }
        public string GardenSurface { get; set; }

        public List<Status> Statuses { get; set; }

        public Status SelectedStatus { get; set; }

        public ResidenceModel()
        {
            Statuses = new List<Status> { Status.Libre, Status.Option, Status.Reserve, Status.Vendu };
        }
    }
}
