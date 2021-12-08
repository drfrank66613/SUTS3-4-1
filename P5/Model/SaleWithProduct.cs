using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4.Model
{
    /** Fill in */
    public class SaleWithProduct
    {
        public int ProdId { get; set; }
        public int SaleId { get; set; }
        public int Qty { get; set; }
        public virtual Product Product { get; set; }
        public virtual Sale Sale { get; set; }
     
    }
}
