using Mindurry.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.Models
{
    public class AcquereursListModel
    {
       
        public string LotNumberArchitect { get; set; }
        public string Type { get; set; }
        public string Stage { get; set; }
        public long Price { get; set; }
        public Contact Contact { get; set; }
    }
}
