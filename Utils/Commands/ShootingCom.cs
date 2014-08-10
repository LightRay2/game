using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.Helpers;

namespace Utils.Commands
{
    public class ComShootToPoint : Command
    {
        public Point2 point;
        public double spreadLevel;
        public int spreadIndex;
        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type, point, spreadIndex, spreadLevel);
        }
        protected override void SetFields(ref List<byte> data)
        {
            point = HEncoder.GetPoint(ref data);
            spreadIndex = HEncoder.GetInt(ref data);
            spreadLevel = HEncoder.GetDouble(ref data);
        }
        public ComShootToPoint(Point2 point, int spreadIndex, double spreadLevel)
        {
            this.point = point;
            this.spreadIndex = spreadIndex;
            this.spreadLevel = spreadLevel;
        }
        public ComShootToPoint() { }
        
    }

    public class ComShootMain : Command
    {
        public Point2 point;
        public double spreadLevel;
        public int spreadIndex;
        public int playerID;
        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type, point, spreadIndex, spreadLevel, playerID);
        }
        protected override void SetFields(ref List<byte> data)
        {
            point = HEncoder.GetPoint(ref data);
            spreadIndex = HEncoder.GetInt(ref data);
            spreadLevel = HEncoder.GetDouble(ref data);
            playerID = HEncoder.GetInt(ref data);
        }
        public ComShootMain(Point2 point, int spreadIndex, double spreadLevel, int playerID)
        {
            this.point = point;
            this.spreadIndex = spreadIndex;
            this.spreadLevel = spreadLevel;
            this.playerID = playerID;
        }
        public ComShootMain() { }

    }

    public class ComSpreadIndex : Command
    {
        public int spreadIndex;
        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type,  spreadIndex);
        }
        protected override void SetFields(ref List<byte> data)
        {
            spreadIndex = HEncoder.GetInt(ref data);
        }
        public ComSpreadIndex(int spreadIndex)
        {
            this.spreadIndex = spreadIndex;
        }
        public ComSpreadIndex() { }
    }

    /*
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

    }*/
}
