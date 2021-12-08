using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceCustomer.Model
{
    public class Customer
    {
        public int CustId { get; set; }
        public String CustName { get; set; }
        public String CustPassword { get; set; }
    }
}
