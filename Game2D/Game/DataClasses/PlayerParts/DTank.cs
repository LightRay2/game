using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.DataClasses;

namespace Game2D.Game.DataClasses
{
    class DTank
    {
        public bool controlled; //управляет ли им наш клиент 
        public int ID;
        public DTankBody body;
        public DLighthouse lighthouse;
        public DShooting shooting = new DShooting();
    }
}
