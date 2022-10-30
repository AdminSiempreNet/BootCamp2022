using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIENDA.Data.Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        //Propiedades de Navegacion
        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BillingDetail> Details { get; set; }
    }
}
