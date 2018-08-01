using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.Models
{
    
    public class ContactsListModel
    {
        public string ContactId { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Commercial { get; set; }
        public DateTimeOffset? LastRelaunch { get; set; }
        public DateTimeOffset? NextRelaunch { get; set; }
        public string Residence { get; set; }
        public string Type { get; set; }
        public int Index { get; set; }

    }
}
