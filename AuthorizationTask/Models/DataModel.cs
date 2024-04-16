using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorizationTask.Models
{
    public class DataModel
    {
        public string name { get; set; }
        public string gender { get; set; }
        public string qul { get; set; }
        public long mobno { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public string add { get; set; }

        //propertyt to change password
        public string npass { get; set; }

    }
}