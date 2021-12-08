using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceBasket.Model
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
