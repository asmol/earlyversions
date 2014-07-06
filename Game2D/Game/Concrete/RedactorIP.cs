using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.Abstract;
using Game2D.Game.DataClasses;
using Game2D.Opengl;

namespace Game2D.Game.Concrete
{
    class RedactorIP
    {
        const EFont mainFont = EFont.lilac;
        const EFont selectedFont = EFont.green;

        public Point2 Location { get; set; }

        ConnectionInfo _info;
        int selected = 0;
        public RedactorIP()
        {
            
            Location = new Point2(50,10);
        }
        public void Process(ref Frame frame, IGetKeyboardState keyboard, ConnectionInfo info)
        {
            _info = info;
            if (keyboard != null)
            {
                if (keyboard.GetActionTime(EKeyboardAction.Fire) == 1)
                {
                    selected = (selected + 1) % 12;
                }
                if (keyboard.GetActionTime(EKeyboardAction.D1) == 1) D(1);
                if (keyboard.GetActionTime(EKeyboardAction.D2) == 1) D(2);
                if (keyboard.GetActionTime(EKeyboardAction.D3) == 1) D(3);
                if (keyboard.GetActionTime(EKeyboardAction.D4) == 1) D(4);
                if (keyboard.GetActionTime(EKeyboardAction.D5) == 1) D(5);
                if (keyboard.GetActionTime(EKeyboardAction.D6) == 1) D(6);
                if (keyboard.GetActionTime(EKeyboardAction.D7) == 1) D(7);
                if (keyboard.GetActionTime(EKeyboardAction.D8) == 1) D(8);
                if (keyboard.GetActionTime(EKeyboardAction.D9) == 1) D(9);
                if (keyboard.GetActionTime(EKeyboardAction.D0) == 1) D(0);
            }

            int selectedSymbol = selected + selected / 3;
            string s="";
            foreach (byte part in info.ip.GetAddressBytes()) s += part.ToString("D3")+'.';
            char[] unselectedText = s.Substring(0,15).ToArray();
            unselectedText[selectedSymbol] = ' ';
            string selectedText = s.Substring(selectedSymbol, 1);
            
            frame.Add(new Text(mainFont, Location,Config.LetterSize3.x, Config.LetterSize3.y,new String(unselectedText)));
            frame.Add(new Text(selectedFont, new Point2(Location.x + selectedSymbol*Config.LetterSize3.x, Location.y),
                Config.LetterSize3.x, Config.LetterSize3.y, selectedText)); 
        }

        void D(int digit)
        {
            byte[] cur = _info.ip.GetAddressBytes();
            int i = selected / 3;
            char[] str = cur[i].ToString("D3").ToArray();
            str[selected%3] = (char)(digit+48);
            byte newByte;
            bool success = byte.TryParse(new String(str), out newByte);
            if (success)
            {
                selected = (selected + 1) % 12;
                cur[i] = newByte;
            }

            _info.ip = new System.Net.IPAddress(cur);

        }
    }
}
