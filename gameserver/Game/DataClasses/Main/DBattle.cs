using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.DataClasses;

namespace gameserver.Game.DataClasses
{
    class DBattle
    {
        public List<DPlayer> players = new List<DPlayer>();
        public List<DShell> shells = new List<DShell>();
    }
}
