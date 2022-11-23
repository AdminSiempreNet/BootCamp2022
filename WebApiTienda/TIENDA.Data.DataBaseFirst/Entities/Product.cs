using System;
using System.Collections.Generic;

#nullable disable

namespace TIENDA.Data.DataBaseFirst.Entities
{
    public partial class Product
    {
        public Product()
        {
            BillingDetails = new HashSet<BillingDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int? Stok { get; set; }
        public string Code { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<BillingDetail> BillingDetails { get; set; }
    }
}
