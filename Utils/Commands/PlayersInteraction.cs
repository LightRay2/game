using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.Helpers;

namespace Utils.Commands
{
    

    public class ComAddPlayer : Command
    {
        public int playerID;
        public string nickname;
        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type, playerID, nickname);
        }
        protected override void SetFields(ref List<byte> data)
        {
            this.playerID = HEncoder.GetInt(ref data);
            this.nickname = HEncoder.GetString(ref data);
        }
        public ComAddPlayer(int playerID,
            string nickname)
        {
            this.playerID = playerID;
            this.nickname = nickname;
        }
        public ComAddPlayer() { }
    }

    public class ComRemovePlayer : Command
    {
        public int playerID;

        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type, playerID);
        }
        protected override void SetFields(ref List<byte> data)
        {
            playerID = HEncoder.GetInt(ref data);
        }
        public ComRemovePlayer(byte playerID)
        {
            this.playerID = playerID;
        }
        public ComRemovePlayer() { }
    }

    public class ComIngameChat : Command
    {
        public int Id;
        public string Message;

        public ComIngameChat() { }

        public ComIngameChat(int id, string message)
        {
            Id = id;
            Message = message;
        }

        protected override void SetFields(ref List<byte> data)
        {
            Id = HEncoder.GetInt(ref data);
            Message = HEncoder.GetString(ref data);
        }

        public override List<byte> GetByteData()
        {
            return HEncoder.Encode(base.type,Id,Message);
        }
    }
}
