using Mindurry.DataModels;
using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.Models
{
    public class ClientPropertyModel
    {
        public string ResidenceName { get; set; }
        public  string PropertyType { get; set; }
        public string PropertyNumber { get; set; }
        public string ApartmentType { get; set; }
        public string Area { get; set; }
        public string CommandState { get; set; }
        public double? Price { get; set; }
        public string Stage { get; set; }
        public string PropertyId { get; set; }

    }
}
