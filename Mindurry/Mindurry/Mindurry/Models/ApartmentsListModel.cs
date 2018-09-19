using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mindurry.Models
{
    public class ApartmentsListModel
    {

        public double TerraceArea { get; set; }
        public double GardenArea { get; set; }
        public string Client { get; set; }

        public Apartment Apartment { get; set; }

    }
}
