using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialtapAPI
{
    public interface IBar
    {
        int Id { get; set; }
        string Name { get; set; }

        string Adress { get; set; }
    }
}
