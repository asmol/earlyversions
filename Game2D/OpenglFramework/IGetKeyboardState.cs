using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Opengl
{
    interface IGetKeyboardState
    {
        //узнать время, в течение которого нажата кнопка действия клавиатуры
        int GetActionTime(EKeyboardAction action);
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
