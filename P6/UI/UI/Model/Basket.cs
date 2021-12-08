using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Model
{
    public class Basket
    {
        public int BasketId { get; set; }
        public String BasketStatus { get; set; }
        public int Qty { get; set; }
        public int CustId { get; set; }

        public String ProdName { get; set; }
        public Double ProdPrice { get; set; }
    }
}
