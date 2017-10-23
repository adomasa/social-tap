using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace social_tap
{
    public class BarInfo
    {
        public int amount, sum;
        public BarInfo()
        {

        }

        public BarInfo(int n, int beverageLevel)
        {
            amount = n;
            sum = beverageLevel;
        }


        public void Count(int n, int beverageLevel)
        {
            amount += 1;
            sum += beverageLevel;
        }
    }
}
