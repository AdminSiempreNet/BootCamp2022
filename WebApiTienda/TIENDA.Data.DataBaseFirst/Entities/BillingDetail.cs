using System;
using System.Collections.Generic;

#nullable disable

namespace TIENDA.Data.DataBaseFirst.Entities
{
    public partial class BillingDetail
    {
        public int Id { get; set; }
        public int BillingId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Billing Billing { get; set; }
        public virtual Product Product { get; set; }
    }
}
