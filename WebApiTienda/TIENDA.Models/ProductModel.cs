using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIENDA.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? Stok { get; set; }
        public string Code { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
       
        public decimal? Margin => Price - Cost;

        public int UnitsSold { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
