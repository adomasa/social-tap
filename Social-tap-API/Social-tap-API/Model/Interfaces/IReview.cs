using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialtapAPI
{
    public interface IReview 
    {
        int BarId { get; }
        int Rate { get; set; }
        string Comment { get; set; } 
    }
}
