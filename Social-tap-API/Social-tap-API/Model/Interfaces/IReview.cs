using Social_Tap_Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialtapAPI
{
    public interface IReview 
    {
        int Id { get; }
        int Rate { get; set; }
        string Comment { get; set; }
        Bar Bar { get; set; }
    }
}
