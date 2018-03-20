using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.DataModels
{
    public class Person
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; } = "95 rue Leclerc - 64600 ANGLET";
        public string Commercial { get; set; }
    }
}
