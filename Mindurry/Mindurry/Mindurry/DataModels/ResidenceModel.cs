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
        public ResidenceModel()
        {
            Statuses = new List<Status> { Status.Libre, Status.Option, Status.Reserve, Status.Vendu };
        }

        public ResidenceModel(Models.DataObjects.Apartment apartment, Residence residence)
        {
            Apartment = apartment;
            Residence = residence;

            Parent = residence.Name;
            NoArchi = apartment.LotNumberArchitect;
            Type = apartment.Kind;
            NoCoPro = apartment.LotNumberCopro;

            //Status _stat;
            //if (Enum.TryParse<Status>(apartment.CommandState, out _stat))
            //Status = SelectedStatus = _stat;

            Exposure = apartment.Exposure;
            Price = apartment.Price;
            Client = apartment.ResidenceName;
            Floor = apartment.Floor;

            Surface = apartment.Area;
            Stadium = apartment.Stage;      

            Statuses = new List<Status> { Status.Libre, Status.Option, Status.Reserve, Status.Vendu };
        }

        public Residence Residence { get; set; }
        public Models.DataObjects.Apartment Apartment { get; set; }
        public Garden _Garden { get; set; }
        public Terrace _Terrace { get; set; }

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
        
        public string NoArchi { get; set; }
        public string Type { get; set; }
        public Status Status { get; set; }
        public string Exposure { get; set; }
        public double Surface { get; set; }
        public double Terace { get; set; }
        public double Garden { get; set; }
        public double Floor { get; set; }
        public double Price { get; set; }
        public string Client { get; set; }
        public string Stadium { get; set; }
        public string NoCoPro { get; set; }
       
        public string PlanFileName { get; set; }
        public List<string> Terraces { get; set; }
        public string GardenSurface { get; set; }

        public List<Status> Statuses { get; set; }

        public Status SelectedStatus { get; set; } 
    }
}
