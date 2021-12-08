using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceProduct.Model
{
    public class Product
    {
        public int ProdId { get; set; }
        public String ProdName { get; set; }
        public Double ProdPrice { get; set; }
    }
}
