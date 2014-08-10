using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Helpers
{
    public class Rand
    {
        static Random instance;
        public static Random e {
            get
            {
                if (instance == null) instance = new Random();
                return instance;
            }
        }
    }
}
