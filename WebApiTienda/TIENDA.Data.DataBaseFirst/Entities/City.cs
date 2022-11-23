using System;
using System.Collections.Generic;

#nullable disable

namespace TIENDA.Data.DataBaseFirst.Entities
{
    public partial class City
    {
        public City()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
