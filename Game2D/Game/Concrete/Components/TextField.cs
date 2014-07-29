using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Game2D.Game.Abstract;
using Game2D.Game.DataClasses;
using Game2D.Opengl;

namespace Game2D.Game.Concrete
{
    class TextField : Component
    {
        private const byte flashMaxValue = 32;
        private readonly short[] specialCharacters = new Int16[] {8,32,37,39,127};

        private int cursorPosition = 0,
            flashValue = flashMaxValue;
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
                if (ConfigOpengl.FontLetters.IndexOf(message[i]) == -1 && !Array.Exists(specialCharacters,value => value == (short)message[i]))
                    message = message.Remove(i,1);
            return message;
        }

        private void handleKeys(string keys)
        {
            foreach (int key in keys)
                switch (key)
                {
                    case 8: // Backspace
                        if (cursorPosition != 0)
                        {
                            text = text.Remove(cursorPosition-1,1);
                            cursorPosition--;
                        }
                        break;
                    case 37: // Left Arrow
                        if (cursorPosition != 0)
                            cursorPosition--;
                        break;
                    case 39: // Right Arrow
                        if (cursorPosition < text.Length)
                            cursorPosition++;
                        break;
                    case 127: // Delete
                        if (cursorPosition < text.Length)
                            text = text.Remove(cursorPosition,1);
                        break;
                    default:
                        if (text.Length < maxLength)
                        {
                            text = text.Insert(cursorPosition,((char)key).ToString());
                            cursorPosition++;
                        }
                        break;
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