using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TIENDA.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int? Stok { get; set; }
        public string Code { get; set; }
        public decimal? Cost { get; set; }
        public decimal?  Price { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<BillingDetail> Details { get; set; }
    }
}
