using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceFeedback.Model
{
    public class Feedback
    {
        public int FeedId { get; set; }
        public String FeedBody { get; set; }
        public int CustId { get; set; }
    }
}
