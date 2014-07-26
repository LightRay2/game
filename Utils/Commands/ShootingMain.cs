using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.Helpers;

namespace Utils.Commands
{
    public class ShootToPoint : Command
    {
        
        public Point2 point;
        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type, point);
        }
        protected override void SetFields(ref List<byte> data)
        {
            point = HEncoder.GetPoint(ref data);
        }
        public ShootToPoint(Point2 point)
        {
            this.point = point;
        }
        public ShootToPoint() { }
        
    }

    public class ShellPath : Command
    {
        public KeyValuePair<int, Point2>[] acc; //момент времени и ускорение. 
        public override List<byte> GetByteData()
        {
            List<object> accObj = new List<object>();
            foreach (var a in acc) { accObj.Add(a.Key); accObj.Add(a.Value); }
            return HEncoder.Encode(base.type, acc.Length, accObj.ToArray());
        }
        protected override void SetFields(ref List<byte> data)
        {
            int n = HEncoder.GetInt(ref data);
            acc = new KeyValuePair<int, Point2>[n];
            for (int i = 0; i < n; i++) 
                acc[i] = new KeyValuePair<int,Point2>(HEncoder.GetInt(ref data), HEncoder.GetPoint(ref data));
        }
        public ShellPath(KeyValuePair<int, Point2>[] acc)
        {
            this.acc = acc;
        }
        public ShellPath() { }

    }
}
