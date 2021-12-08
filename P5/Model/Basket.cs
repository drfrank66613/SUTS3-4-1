using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4.Model
{
    /** Fill in */
    public class Basket
    {
        public int BasketId { get; set; }
        public string BasketStatus { get; set; }
        public int CustId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<BasketWithProduct> BasketWithProducts { get; set; }
    }
}
