using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.DataClasses
{
    public class DGun
    {
        public Vector2 relPos; //точка - позиция относительно центра танка, вектор - место вылета снарядов(меняется при повороте башни)

        public readonly double ACC_FIRST = 0.02, ACC_SECOND = -0.002;
        public readonly double DIST_PART_OF_FIRST_ACC = 0.2;
        public readonly double END_SPEED_PART_OF_MAX = 0.4;//насколько конечная скорость отличается от максимальной
        public readonly double MAX_RANGE = 100,
            MAX_SPREAD = 2, //проценты
            MAX_ANGLE_SPREAD = 4; //градусы

        //public 
    }
}
