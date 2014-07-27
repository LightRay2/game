using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Boxes;

namespace Game2D.Game.DataClasses
{
    class DLighthouse
    {
        public const double distBetweenPoints = 0.5;
        public const int nearestPoint = 5;

        public int sightCount=4;
        public int pointMaxCount=300;
        public int currentSight = 0;
        public Vector2 pos = new Vector2(0,0,0); //относительно

        public int firstSight=60, lastSight=200; //место первого и последнего прицела


        public ValueBox ellipseTime = new ValueBox() { max = 5000, min = 0, minus = 40, plus = 1, now = 5000 };
        public ValueBox ellipseSize = new ValueBox() { max = 1.0, min = 0.3, minus = 0.01, plus = 0.02, now =1.0 };

        public void ChangeCurrentSight(){
            currentSight = (currentSight + 1) % sightCount;
        }

        public void MoveLastSight(bool farther)
        {
            if (farther) lastSight = Math.Min(lastSight + 1, pointMaxCount);
            else lastSight = Math.Max(lastSight - 1, firstSight+ sightCount-1);
        }

        public void MoveFirstSight(bool farther)
        {
            if (farther) firstSight = Math.Min(lastSight - sightCount + 1, firstSight + 1);
            else firstSight = Math.Max(firstSight - 1, nearestPoint);
        }

        public Vector2 GetSightPos(Point2 start, int num)
        {
            if (num < 0 && num >= sightCount) throw new Exception();
            int pointNum = num==sightCount-1? lastSight: firstSight + (lastSight-firstSight)/(sightCount-1)*num;
            return new Vector2(start +
                new Vector2(new Point2(0, 0), pos.angleDeg, distBetweenPoints * pointNum).vector, pos.angleDeg, 1);
        }
    }
}
