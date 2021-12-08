using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4.Model
{
    /** Fill in */
    public class BasketWithProduct
    {
        public String Name { get; set; }
        public int BasketId { get; set; }
        public int ProdId { get; set; }
        public int Qty { get; set; }
        public virtual Basket Basket { get; set; }
        public virtual Product Product { get; set; }
    }
}
