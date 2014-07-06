using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.DataClasses;
using Game2D.Game.Abstract;
using Game2D.Opengl;

namespace Game2D.Game.Concrete
{
    class MenuMain
    {
        #region переменные и конструктор
        const EFont MAIN_FONT = EFont.fiol;
        const EFont SELECTED_FONT = EFont.orange;
        readonly Point2 POS = new Point2(50, 20);
        readonly Point2 TANK_POS = new Point2(25, 80);

        MenuItem _itemsHead ;
        ConnectionInfo _connectionInfo;
        BattleConfig _battleConfig;
        RedactorIP _redactorIp ;

        int _time = 0;
        bool _showTankRotation = false,_showIpMenu = false;
        int selected=0;
        MenuItem _active;
        public MenuMain()
        {
            _redactorIp = new RedactorIP();

            _itemsHead = new MenuItem("", 
                new MenuItem("Присоединиться к серверу", ConnectToServer),
                new MenuItem("Выбор танка", ChangeTankVisability,1,
                    new MenuItem("Разящий",ChangeTank,0),
                    new MenuItem( "Раздавливающий",ChangeTank,1),
                    new MenuItem( "Уничтожающий",ChangeTank,2)
                    ),
                new MenuItem("Задать IP-адрес сервера",ChangeIpMenuVisability,1,
                    new MenuItem("Сохранить",ChangeIpMenuVisability,0)
                    )
                );
            SetParents(_itemsHead);
            _active = _itemsHead;
        }
        #endregion

        #region Основной процесс
        public void Process(ref Frame frame, IGetKeyboardState keyboard,
            ConnectionInfo connectionInfo, BattleConfig battleConfig)
        {
            _connectionInfo = connectionInfo;
            _battleConfig = battleConfig;

            if (keyboard != null)
            {
                if (keyboard.GetActionTime(EKeyboardAction.Enter) == 1)
                {
                    MenuItem m = _active.items[selected];
                    if (_active.items[selected].items.Count > 0)
                    {
                        _active = _active.items[selected];
                        selected = 0;
                    }
                    m.Do();
                }
                if (keyboard.GetActionTime(EKeyboardAction.Esc) == 1)
                {
                    PreviousMenu();
                }
                if(keyboard.GetActionTime(EKeyboardAction.Fire)==1)
                    selected = (selected+1)%_active.items.Count;
            }

            frame.Add(new Sprite(ESprite.menuback, Config.ScreenWidth, Config.ScreenHeight, new Vector2(Config.ScreenWidth/2,Config.ScreenHeight/2,0)));

            Point2 curPos = POS;
            for (int i = 0; i < _active.items.Count; i++)
            {
                EFont font = i == selected ? SELECTED_FONT : MAIN_FONT;
                frame.Add(new Text(font, curPos, Config.LetterSize3.x, Config.LetterSize3.y, _active.items[i].text));
                curPos.y += Config.LetterSize3.y;
            }

            if (_showIpMenu) _redactorIp.Process(ref frame, keyboard, connectionInfo);
            
            ESprite spr = battleConfig.tank == BattleConfig.ETank.first? ESprite.tank0 : 
                (battleConfig.tank==BattleConfig.ETank.second ? ESprite.tank1 : ESprite.tank2);
            frame.Add(new Sprite(spr, 20, 10, new Vector2(TANK_POS.x, TANK_POS.y, _time % 360)));
            
            _time++;
        }
        #endregion

        //эта штука вызывается, когда жмем коннект
        //айпи адрес находится в: _connectionInfo.ip
        void ConnectToServer()
        {
            _connectionInfo.allowConnect = true;
        }

        #region Мелкие вспомогательные функции
        void ChangeIpMenuVisability(int a)
        {
            _showIpMenu = a == 0 ? false : true;
            if (a == 0) PreviousMenu();
        }

        void ChangeTankVisability(int a)
        {
            _showTankRotation = a == 0 ? false : true;
        }

        void ChangeTank(int num)
        {
            _battleConfig.tank = (BattleConfig.ETank)num;
            ChangeTankVisability(0);
            PreviousMenu();
        }

        void SetParents(MenuItem node)
        {
            foreach (MenuItem child in node.items)
            {
                SetParents(child);
                child.parent = node;
            }
        }

        void PreviousMenu()
        {
            if (_active.parent != null) 
                _active = _active.parent;
            selected = 0;
        }
        #endregion
    }
}
