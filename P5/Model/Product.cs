using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4.Model
{
    /** Fill in */
    public class Product
    {
        public int ProdId { get; set; }
        public String ProdName { get; set; }
        public Double ProdPrice { get; set; }
        public virtual ICollection<BasketWithProduct> BasketWithProducts { get; set; }
        public virtual ICollection<SaleWithProduct> SaleWithProducts { get; set; }
    }
}
