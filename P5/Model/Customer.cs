using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4.Model
{
    /** Fill in */
    public class Customer
    {
        public int CustId { get; set; }
        public String CustName { get; set; }
        public String CustPassword { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }

    }
}
