﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceSale.Model
{
    public class Sale
    {
        public int SaleId { get; set; }
        public String SaleStatus { get; set; }
        public int CustId { get; set; }

        public String ProdName { get; set; }
        public Double ProdPrice { get; set; }

    }
}