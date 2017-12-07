using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialtapAPI;

namespace Social_Tap_Api
{
    public class Review: IReview 
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public Bar Bar  { get; set; }
       
    }
}
