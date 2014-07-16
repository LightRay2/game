﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D
{
    class Config
    {
        public const double ScreenWidth = ConfigOpengl.ScreenWidth;
        public const double ScreenHeight = ConfigOpengl.ScreenHeight;
        public const int TimePerFrame = ConfigOpengl.TimePerFrame; //в миллисекундах

        public static Point2 LetterSize1 = new Point2(1, 2);
        public static Point2 LetterSize2 = new Point2(2, 4);
        public static Point2 LetterSize3 = new Point2(3, 6);
    }
}
