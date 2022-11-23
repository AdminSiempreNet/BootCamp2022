using System;
using System.Collections.Generic;

#nullable disable

namespace TIENDA.Data.DataBaseFirst.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Billings = new HashSet<Billing>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CityId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Billing> Billings { get; set; }
    }
}
