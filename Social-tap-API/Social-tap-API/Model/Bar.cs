using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialtapAPI;

namespace Social_Tap_Api
{
    public class Bar:IBar 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public Review Review { get; set; }

        public int BarId { get;set; }
    }
}
