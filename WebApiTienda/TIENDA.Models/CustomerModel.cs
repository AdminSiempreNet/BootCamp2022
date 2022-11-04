using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIENDA.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public int BillingCount { get; set; }
        public DateTime? LastBuy { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class CustomerPostModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
