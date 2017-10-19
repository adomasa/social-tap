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
        /*public BarInfo(int n, int beverageLevel)
        {
            amount = n;
            sum = beverageLevel;
        }
       
        */

        public Lazy<BarInfo> _barinfo;

        public BarInfo(int sum)
        {
            this.sum = sum;
        }

        public BarInfo(int n, int beverageLevel)
        {
            amount = n;
            _barinfo = new Lazy<BarInfo>(() =>
            {
                return new BarInfo(this.sum);
            });
        }
        public void Count(int n, int beverageLevel)
        {
            amount += 1;
            sum += beverageLevel;
        }

    }
}
