using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Game.DataClasses
{
    class DLighthouse
    {
        public const double distBetweenPoints = 0.5;
        public const int nearestPoint = 5;

        public int SightCount=4;
        public int pointMaxCount=150;
        public Vector2 pos = new Vector2(0,0,0); //относительно

        public int firstSight=60, lastSight=100; //место первого и последнего прицела

    }
}
