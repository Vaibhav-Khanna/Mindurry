using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mindurry.DataModels
{
    class ResidenceCollection
    {
        public string Name { get; set; }

        public string Comment { get; set; }

        //This is for child List
        public ObservableCollection<Apartment> ApartmentItems { get; set; }



    }
}
