﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D
{
    struct Sprite
    {
        public ESprite name;
        public int frame;
        public double width, height;
        public Vector2 pos;
        /// <summary>
        /// это поле менять не желательно, само выставится как надо
        /// </summary>
        public string texture;

        /// <param name="name">какой спрайт</param>
        /// <param name="frame">номер кадра, если анимация. Начинаем с 0</param>
        /// <param name="pos">Где находится спрайт и угол поворота</param>
        /// <param name="width">ширина</param>
        /// <param name="height">высота</param>
        public Sprite(ESprite name, Vector2 pos, double width, double height, int frame = 0)
        {
            this.name = name;
            this.frame = frame;
            this.width = width;
            this.height = height;
            this.pos = pos;
            this.texture = name.ToString();
        }

        /// <param name="name">какой спрайт</param>
        /// <param name="frame">номер кадра, если анимация. Начинаем с 0</param>
        /// <param name="pos">Где находится спрайт и угол поворота</param>
        /// <param name="size">размер</param>
        public Sprite(ESprite name, Vector2 pos, Point2 size, int frame = 0)
        {
            this.name = name;
            this.frame = frame;
            this.width = size.x;
            this.height = size.y;
            this.pos = pos;
            this.texture = name.ToString();
        }

    }
}
