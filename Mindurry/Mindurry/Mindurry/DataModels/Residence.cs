using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.DataModels
{
    public enum Statut { Libre, Reserve, Vendu}

    public class Residence
    {
        public string Parent { get; set; }
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
        public double NoArchi { get; set; }
        public Statut Statut { get; set; }
        public string Exposition { get; set; }
        public double Surface { get; set; }
        public bool Terasse { get; set; }
        public bool Jardin { get; set; }
        public double Etage { get; set; }
        public double Prix { get; set; }
    }
}
