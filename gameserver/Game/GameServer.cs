using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Commands;
using gameserver.Game.Concrete;
using gameserver.Game.DataClasses;

namespace gameserver.Game
{
    class GameServer
    {
        PlayerController _playerController = new PlayerController();
        SShellController _shellController = new SShellController();
        DStateMain _state = new DStateMain();

        public List<Command> Process(List<Command> commands)
        {
            List<Command> r = new List<Command>();

            r.AddRange(_playerController.Process(commands,_state));
            r.AddRange(_shellController.Process(commands, _state));
            double[] x= Utils.Helpers.HRandomDoubleGroup.GetRandomGroup(199);
            return r;
        }
    }
}
