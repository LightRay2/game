using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game2D.Game.Concrete;
using Game2D.Game.DataClasses;
using Game2D.Opengl;
using Utils.Commands;

namespace Game2D.Game
{
    class ChatManager
    {
        private DChat chat = new DChat();
        private TextField field = new TextField(DChat.X,DChat.Y+DChat.MaxMessages*2);

        public List<Command> Process(List<Command> commands, DStateMain state, IGetKeyboardState kstate, ref Frame frame)
        {
            List<Command> result = new List<Command>();
            if (state.battle != null)
            {
                addMessages(commands,state);
                frame.Add(new Text(field.Font,DChat.X,DChat.Y,field.Size.Width,field.Size.Height,chat.Messages));
                if (kstate.GetActionTime(EKeyboardAction.Chat) == 1)
                    field.Visible = !field.Visible;
                field.Paint(kstate,ref frame);
                if (field.Visible && kstate.GetActionTime(EKeyboardAction.Enter) == 1)
                {
                    result.Add(new ComIngameChat(state.battle.me.id,field.Text));
                    field.Visible = false;
                    field.Text = String.Empty;
                }
            }
            return result;
        }

        private void addMessages(List<Command> commands, DStateMain state)
        {
            bool justConnected = false;
            foreach (Command cmd in commands)
            {
                if (cmd is ComConnectionResult)
                    justConnected = true;
                else if (cmd is ComAddPlayer)
                {
                    ComAddPlayer c = cmd as ComAddPlayer;
                    if (!justConnected)
                        chat.AddMessage(String.Format("{0} connected.",c.nickname));
                }
                else if (cmd is ComRemovePlayer)
                {
                    ComRemovePlayer c = cmd as ComRemovePlayer;
                    if (c.playerID != state.battle.me.id)
                        chat.AddMessage(String.Format("{0} disconnected.",getNameFromId(c.playerID,state.battle.players)));
                }
                else if (cmd is ComIngameChat)
                {
                    ComIngameChat c = cmd as ComIngameChat;
                    chat.AddMessage(String.Format("{0}: {1}",getNameFromId(c.Id,state.battle.players),c.Message));
                }
            }
        }

        private string getNameFromId(int id, List<DPlayer> players)
        {
            return (from player in players where id == player.id select player).First().nickname;
        }
    }
}