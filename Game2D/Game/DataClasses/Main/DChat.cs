using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Game.DataClasses
{
    class DChat
    {
        private string[] messages = new String[MaxMessages];

        public const int MaxMessages = 15,
            X = 1,
            Y = 1;

        public string[] Messages
        {
            get
            {
                string[] temp = (string[])messages.Clone();
                Array.Reverse(temp);
                return temp;
            }
        }

        public DChat()
        {
            for (int i = 0; i < messages.Length; i++)
                messages[i] = String.Empty;
        }

        public void AddMessage(string message)
        {
            for (int i = messages.Length-1; i > 0; i--)
                messages[i] = messages[i-1];
            messages[0] = message;
        }
    }
}