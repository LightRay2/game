using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

using Game2D.Game.Abstract;
using Game2D.Game.DataClasses;
using Game2D.Opengl;

namespace Game2D.Game.Concrete
{
    class TextField : Component
    {
        private const byte flashMaxValue = 32;
        private readonly byte[] specialCharacters = new Byte[] {8,32,127,148,150,154,155,156};

        private int cursorPosition = 0,
            flashValue = flashMaxValue;
        private bool replace = false;
        private EFont font = EFont.green;
        private string text = String.Empty;
        private int maxLength = 96;

        public EFont Font
        {
            get {return font;}
            set {font = value;}
        }
        public string Text
        {
            get {return text;}
            set
            {
                if (text.Length <= maxLength)
                {
                    text = value;
                    cursorPosition = text.Length;
                }
            }
        }
        public int MaxLength
        {
            get {return maxLength;}
            set
            {
                if (value > 0)
                    maxLength = value;
            }
        }

        public TextField(int x, int y)
        {
            Position = new Point(x,y);
            Size = new Size(1,2);
        }

        public override void Paint(IGetKeyboardState state, ref Frame frame)
        {
            if (Visible)
            {
                handleKeys(clearString(state.GetEnteredString()));
                frame.Add(new Text(font,Position.X,Position.Y,Size.Width,Size.Height,flashCursor()));
            }
        }

        private string clearString(string message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                byte code = convertKey(Encoding.GetEncoding(1251).GetBytes(new Char[] {message[i]})[0]);
                if (ConfigOpengl.FontLetters.IndexOf(message[i]) == -1 && !Array.Exists(specialCharacters,value => value == code))
                    message = message.Remove(i,1);
                if (code == 145 || code == 147)
                    message = message.Insert(i,Encoding.GetEncoding(1251).GetString(new Byte[] {code}));
            }
            return message;
        }

        private byte convertKey(byte key)
        {
            switch (key)
            {
                case 34: key = 147; break;
                case 39: key = 145; break;
            }
            return key;
        }

        private void handleKeys(string keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                byte key = Encoding.GetEncoding(1251).GetBytes(new Char[] {keys[i]})[0];
                switch (key)
                {
                    case 8: // Backspace
                        if (cursorPosition != 0)
                        {
                            text = text.Remove(cursorPosition-1,1);
                            cursorPosition--;
                        }
                        break;
                    case 127: // Delete
                        if (cursorPosition < text.Length)
                            text = text.Remove(cursorPosition,1);
                        break;
                    case 148: // Left Arrow
                        if (cursorPosition != 0)
                            cursorPosition--;
                        break;
                    case 150: // Right Arrow
                        if (cursorPosition < text.Length)
                            cursorPosition++;
                        break;
                    case 154: cursorPosition = 0; break; // Home
                    case 155: cursorPosition = text.Length; break; // End
                    case 156: replace = !replace; break; // Insert
                    default:
                        if (text.Length < maxLength)
                        {
                            string c = Encoding.GetEncoding(1251).GetString(new Byte[] {key});
                            if (replace && cursorPosition != text.Length)
                                text = text.Remove(cursorPosition,1);
                            text = text.Insert(cursorPosition,c);
                            cursorPosition++;
                        }
                        break;
                }
            }
        }

        private string flashCursor()
        {
            StringBuilder builder = new StringBuilder(text);
            builder.Length = text.Length+1;
            if (flashValue > flashMaxValue/2)
                builder[cursorPosition] = '_';
            flashValue -= 1;
            if (flashValue == 1)
                flashValue = flashMaxValue;
            return builder.ToString();
        }
    }
}