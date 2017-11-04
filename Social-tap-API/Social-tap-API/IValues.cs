using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialtapAPI
{
    interface IValues
    {
        Boolean Average(int beverageLevel);
        List<string> HashtagsFinder(string comment);

        Dictionary<string, List<string>> CountBars(string barName, string comment);



    }
}
