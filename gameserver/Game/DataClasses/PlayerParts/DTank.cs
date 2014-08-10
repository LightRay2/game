using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gameserver.Game.DataClasses;
using Utils.DataClasses;

namespace gameserver.Game.DataClasses
{
    class DTank
    {
        public int ID;
        public DTankBody body = new DTankBody(new Vector2(0,0,0));
        public DShooting shooting = new DShooting();
    }
}
