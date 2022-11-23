using System;
using System.Collections.Generic;

#nullable disable

namespace TIENDA.Data.DataBaseFirst.Entities
{
    public partial class Billing
    {
        public Billing()
        {
            BillingDetails = new HashSet<BillingDetail>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int? UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BillingDetail> BillingDetails { get; set; }
    }
}
