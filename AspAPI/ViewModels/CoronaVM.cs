using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace AspAPI.ViewModels
{
    public class CoronaVM
    {
        public string country { get; set; }
        public int cases { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
    }
}