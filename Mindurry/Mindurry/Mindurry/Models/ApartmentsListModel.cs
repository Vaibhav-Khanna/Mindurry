using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.Models
{
    public class ApartmentsListModel
    {

        public long TerrasseArea { get; set; }
        public long GardenArea { get; set; }
        public string Client { get; set; }

        public Apartment Apartment { get; set; }

    }
}
