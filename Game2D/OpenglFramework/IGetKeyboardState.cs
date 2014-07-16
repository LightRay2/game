using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Opengl
{
    /// <summary>
    /// Про клавиши:
    /// везде, где есть латинская буква, код клавиши равен коду большой латинской буквы ('ф'='A' = 65). 
    /// где нет латинской буквы, есть много вариантов - в зависимости от языка и зажат ли шифт.
    /// </summary>
    interface IGetKeyboardState
    {
        //узнать время, в течение которого нажата кнопка действия клавиатуры
        int GetActionTime(EKeyboardAction action);
        /// <summary>
        /// коды, меньшие 128. Если нажато с shift или ctrl, код отличается от простого нажатия.
        /// Русские буквы - большие и маленькие - преобразуются в английские согласно стандартной клавиатуре
        /// </summary>
        List<byte> GetPressedKeys();
        /// <summary>
        /// русские и английские, большие и маленькиe буквы
        /// </summary>
        string GetEnteredString();
        //Свойства мыши
        /// <summary>
        /// на экране, верхний угол это (0,0)
        /// </summary>
        Point2 MousePosScreen { get;  }
        /// <summary>
        /// На карте, с учетом сдвига камеры
        /// </summary>
        Point2 MousePosMap { get; }
        bool MouseClick { get;  }
        bool MouseRightClick { get;  }
    }
}
