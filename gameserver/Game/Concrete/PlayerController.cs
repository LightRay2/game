﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Commands;
using Game2D.Game.DataClasses;

namespace gameserver.Game.Concrete
{
    class PlayerController
    {
        public List<Command> Process(List<Command> commands, DStateMain state)
        {
            List<Command> r = new List<Command>();
            foreach (Command c in commands)
            {
                if (c is ComConnect)
                {
                    ComConnect com = (ComConnect)c;
                    r.Add(new ComConnectionResult(com.clientID, com.nickname));

                    foreach (DPlayer p in state.battle.players)
                    {
                        r.Add(new ComAddPlayer(com.clientID, com.nickname) { clientID = p.id });
                        r.Add(new ComAddPlayer(p.id, p.nickname) { clientID = com.clientID });
                    }

                    state.battle.players.Add(new DPlayer(com.clientID, com.nickname, new DTank()));
                }
            }
            return r;
        }


    }
}
