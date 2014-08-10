using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Commands;
using Utils.Helpers;
using gameserver.Game.DataClasses;
using Utils.DataClasses;

namespace gameserver.Game.Concrete
{
    class SShellController
    {
        public List<Command> Process(List<Command> commands, DStateMain state)
        {
            List<Command> r = new List<Command>();
            foreach (Command c in commands)
            {
                if (c is ComSpreadIndex)
                {
                    ComSpreadIndex com = (ComSpreadIndex)c;
                    r.Add(new ComSpreadIndex(Rand.e.Next(HRandomDoubleGroup.COUNT)) { clientID = c.clientID });
                }
                if (c is ComShootToPoint)
                {
                    ComShootToPoint com = (ComShootToPoint)c; //todo сервер организация - назначать id не по порядку
                    DShooting shooting =  state.battle.players[c.clientID].tank.shooting;
                    shooting.aim = com.point;
                    shooting.spreadLevel = com.spreadLevel;
                    List<DShell> shells = HShells.SetShellsFlight(shooting, com.spreadIndex, state.battle.players[c.clientID].tank.body.pos.point);
                    state.battle.shells.AddRange(shells);
                    shooting.shells.AddRange(shells);

                    foreach (DPlayer player in state.battle.players)
                    {
                        r.Add(new ComShootMain(com.point, com.spreadIndex, com.spreadLevel, com.clientID) { clientID = player.id });
                        
                    }
                    //todo перезарядка, проверка индекса, проверка числа снарядов
                }
            }

            for(int i = 0; i < state.battle.shells.Count; i++)
            {
                DShell shell = state.battle.shells[i];
                HShells.MoveShell(shell);
                if (shell.time == shell.timeFirst + shell.timeSecond)
                {
                    shell.Destroy();
                    state.battle.shells.RemoveAt(i--);
                }
            }


            HShells.Collisions(new List<KeyValuePair<object, List<Vector2>>>());
            return r;
        }

        
              
        
    }
}
