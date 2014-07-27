using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Boxes
{
    public class ValueBox
    {
        public double max, min, now, plus, minus;

        public bool Decrease()
        {
            if (now - minus < min)
                return false;

            now -= minus;
            return true;
        }

        public bool Increase()
        {
            if (now == max)
                return false;


            now  = Math.Min(max, now + plus);
            return true;
        }

        public bool AtMin()
        {
            return now - minus < min;
        }

        public bool AtMax()
        {
            return now + plus ==max;
        }
    }
}
