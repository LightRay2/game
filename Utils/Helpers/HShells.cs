using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.DataClasses;

namespace Utils.Helpers
{
    public class HShells
    {
        public static void MoveShell(DShell shell)
        {
            double acc = shell.time < shell.timeFirst ? shell.gun.ACC_FIRST : shell.gun.ACC_SECOND;
            Vector2 speed = new Vector2(new Point2(0, 0), shell.pos.angleDeg, shell.curSpeed + acc / 2);
            shell.pos = new Vector2(shell.pos.point + speed.vector, shell.pos.angleDeg, 1);
            shell.curSpeed += acc;
            shell.time++;
        }

        public static List<DShell> SetShellsFlight(DShooting shooting, int spreadIndex, Point2 center)
        {
            double[] spread = HRandomDoubleGroup.GetRandomGroup(spreadIndex);
            int curSpread = 0;
            List<DShell> shells = new List<DShell>();
            foreach (DGun gun in shooting.guns)
            {
                Point2 toAim = shooting.aim - (center + gun.relPos.point);
                double angle = gun.MAX_ANGLE_SPREAD * spread[curSpread++]*shooting.spreadLevel + toAim.angleTo(new Point2(1,0));
                double dist = Math.Min(gun.MAX_RANGE, toAim.Length());
                dist += gun.MAX_SPREAD * spread[curSpread++]* shooting.spreadLevel;

                int firstTime = GetTimeOfAcc(dist * gun.DIST_PART_OF_FIRST_ACC, gun.ACC_FIRST, 0);
                int secondTime = GetTimeOfAcc(dist - dist * gun.DIST_PART_OF_FIRST_ACC, gun.ACC_SECOND,
                    firstTime * gun.ACC_FIRST);

                shells.Add(new DShell()
                {
                    pos = new Vector2(gun.relPos.point + center , angle, 1),
                    timeFirst = firstTime,
                    timeSecond = secondTime,
                    parent = shooting,
                    gun = gun
                });


            }
            return shells;
        }



        static int GetTimeOfAcc(double dist, double acc, double startSpeed)
        {
            double D = startSpeed * startSpeed + 2 * acc * dist;
            return (int)Math.Ceiling((-startSpeed + Math.Sqrt(D)) / acc) ;
        }

        public class Body
        {
            public object parent;
            public Vector2 pos;
            public Point2 size;
        }

        public static List<KeyValuePair<object, object>> Collisions(List<Body> objects)
        {
            List<KeyValuePair<object, object>> r = new List<KeyValuePair<object,object>>();

            double tileSize = 50; //todo mapsize
            int n = 20, m = 20; //не забывать, что тут все сдвинуто на 1 вправо и вниз
            List<int>[,] ii = new List<int>[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; i < m; j++)
                    ii[i, j] = new List<int>();

            for(int i = 0; i < objects.Count;i++)
            {
                KeyValuePair<object, List<Vector2>> obj = objects[i];
                foreach (var rect in obj.Value)
                {
                    if(rect.vector.x > tileSize*2 || rect.vector.y > tileSize*2)
                        throw new Exception("Здесь не реализовано столкновение таких больших объектов. Разбейте объект на прямоугольники"
                            +"с максимальным размером "+ (tileSize*2).ToString());

                    int y = (int)((rect.y + rect.vy / 2+1) / tileSize);
                    int x = (int)((rect.x + rect.vx / 2+1) / tileSize);
                    
                    for(int dy = -1; dy <= 1; dy++)
                        for(int dx = -1; dx <= 1; dx++)
                            if(!ii[dy+y, dx+x].Contains(i))
                                ii[dy+y, dx+x].Add(i);
                }
            }

            for(int y = 0; y < n; y++)
                for (int x = 0; x < m; x++)
                {
                    if (ii[y, x].Count >= 2)
                    {
                        for (int i = 0; i < ii[y, x].Count - 1; i++)
                            for (int j = i + 1; j < ii[y, x].Count; j++)
                            {
                                List<Vector2> first = objects[ii[y,x][i]].Value;
                                List<Vector2> second = objects[ii[y, x][j]].Value;
                            }
                    }
                }

            return r;
        }

        static bool Cross(List<Vector2> one, List<Vector2> two)
        {
            foreach(var a in one)
                foreach(var b in two)
        }
    }
}
