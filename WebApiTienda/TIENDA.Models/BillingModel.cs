using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIENDA.Models
{

    public class BillingModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public List<BillingDetailsModel> Details { get; set; } = new List<BillingDetailsModel>();

        public decimal Total
        {
            get
            {
                return Details.Sum(x=>x.Valor);
            }
        }
    }

    public class BillingDetailsModel
    {
        public int Id { get; set; }
        public int BillingId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Valor
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }

    }
}
