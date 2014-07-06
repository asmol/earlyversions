using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Opengl;

namespace Game2D.Game.Abstract
{
    interface IProcess
    {
        //null передаем, если не хотим, чтобы объект реагировал на мышку и клавиатуру
        //например, если он не на первом слое
        void Process(ref Frame frame, IGetKeyboardState keyboard=null);
    }
}
