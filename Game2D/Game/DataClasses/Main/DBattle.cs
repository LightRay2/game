using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.DataClasses;

namespace Game2D.Game.DataClasses
{
    class DBattle
    {
        public List<DPlayer> players = new List<DPlayer>();
        public List<DShell> shells = new List<DShell>();
        //отдельно выделяем нашего игрока, чтоб по 100 раз не бегать по всему списку
        public DPlayer me;
    }
}
