using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGeneration
{

    class AVector3
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public AVector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public AVector3(APoint3 begin, APoint3 end)
        {
            x = end.x - begin.x;
            y = end.y - begin.y;
            z = end.z - begin.z;
        }

        public static AVector3 perpendicular(AVector3 v1, AVector3 v2)
        {
            return new AVector3(
                v1.y * v2.z - v2.y * v1.z,
                v2.x * v1.z - v1.x * v2.z,
                v1.x * v2.y - v2.x * v1.y
                );
        }

        public void turn_up()
        {
            if (z < 0)
            {
                x *= -1;
                y *= -1;
                z *= -1;
            }
        }
    }

    class APoint3
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public APoint3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

}
