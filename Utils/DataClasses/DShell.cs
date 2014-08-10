using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.DataClasses
{
    public class DShell
    {
        public int timeFirst, timeSecond;
        public int explosionFlightTime = 4;
        public double curSpeed;
        public Vector2 pos;
        public int time = 0;
        public DShooting parent;
        public DGun gun; 
        public void Destroy()
        {
            parent.shells.Remove(this);
        }
    }
}
