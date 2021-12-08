using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4.Model
{
    /** Fill in */
    public class Sale
    {
        public int SaleId { get; set; }
        public String SaleStatus { get; set; }
        public int CustId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<SaleWithProduct> SaleWithProducts { get; set; }
    }
}
