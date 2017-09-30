using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace social_tap
{
    public class ComparingClass : IComparer
    {
        int IComparer.Compare(Object value, Object ten)
        {
            int a = (int)value;
            int b = (int)ten;

            int modulo = a % b;

            if (modulo <= 3 && a != b)
                return -1;
            else if (modulo <= 6 && a!=b)
                return 0;
            else
                return 1;

        }
    }
}
