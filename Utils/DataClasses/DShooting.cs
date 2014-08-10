using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.DataClasses
{
    public class DShooting
    {
        public int lastSpreadIndex=-1, lastButOneSpreadIndex=-1;
        public List<DShell> shells = new List<DShell>();
        public Point2 aim;
        public double spreadLevel;

        const double tank = 10;
        public  List<DGun> guns = new List<DGun>{
            new DGun() { relPos =  new Vector2(new Point2(tank/10*4, tank/4), 0,1)},
            new DGun() { relPos =  new Vector2(new Point2(tank/10*2, tank/4), 0,1)},
            new DGun() { relPos =  new Vector2(new Point2(tank/10*0, tank/4), 0,1)},
            new DGun() { relPos =  new Vector2(new Point2(tank/10*(-2), tank/4), 0,1)},
            new DGun() { relPos =  new Vector2(new Point2(tank/10*(-4), tank/4), 0,1)},

            new DGun() { relPos =  new Vector2(new Point2(tank/10*4, -tank/4), 0,1)},
            new DGun() { relPos =  new Vector2(new Point2(tank/10*2, -tank/4), 0,1)},
            new DGun() { relPos =  new Vector2(new Point2(tank/10*0, -tank/4), 0,1)},
            new DGun() { relPos =  new Vector2(new Point2(tank/10*(-2), -tank/4), 0,1)},
            new DGun() { relPos =  new Vector2(new Point2(tank/10*(-4), -tank/4), 0,1)},
    };
    }
}
