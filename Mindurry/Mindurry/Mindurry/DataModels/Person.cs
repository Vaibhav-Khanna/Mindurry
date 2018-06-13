using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Mindurry.DataModels
{
    public enum PersonType { Lead, Contact, Client }
    public class Person
    {

        public PersonType Type { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; } = "95 rue Leclerc - 64600 ANGLET";
        public string Commercial { get; set; }
        public DateTime LastRelaunch { get; set; }
        public DateTime NextRelaunch { get; set; }
        public string Residence { get; set; }
        public string ApartmentType { get; set; }
        public int Index { get; set; }
    }
}
