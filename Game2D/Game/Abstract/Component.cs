using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Game2D.Opengl;

namespace Game2D.Game.Abstract
{
    abstract class Component
    {
        public bool Visible {get; set;}
        public Point Position {get; set;}
        public Size Size {get; set;}

        public abstract void Paint(IGetKeyboardState state, ref Frame frame);
    }
}