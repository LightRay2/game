﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Game.DataClasses.TankParts
{
    class DGun
    {
        public Vector2 relPos; //точка - позиция относительно центра танка, вектор - место вылета снарядов(меняется при повороте башни)

        public double maxRange = 100, 
            maxSpread = 2, //проценты
            maxAngleSpread=4; //градусы
        
        //public 
    }
}
