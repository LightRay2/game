using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Commands;
using gameserver.Game.Concrete;
using Game2D.Game.DataClasses;

namespace gameserver.Game
{
    class GameServer
    {
        PlayerController _playerController = new PlayerController();
        DStateMain _state = new DStateMain();

        public List<Command> Process(List<Command> commands)
        {
            List<Command> r = new List<Command>();

            r.AddRange(_playerController.Process(commands,_state));
            return r;
        }
    }
}
