using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TIENDA.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public string PhoneNumber{ get; set; }
        public string Email { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
