using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4.Model
{
    /** Fill in */
    public class Feedback
    {
        public int FeedId { get; set; }
        public String FeedBody { get; set; }
        public int CustId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
