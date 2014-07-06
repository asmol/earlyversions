using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2D.Game.DataClasses
{
    class MenuItem
    {
        public delegate void Action();
        public delegate void ActionP(int parameter);
        

        Action Act = null;
        ActionP ActP = null;
        public string text;
        public bool selected;
        public int parameter;
        public List<MenuItem> items;
        public MenuItem parent = null;

        /// <summary>
        /// конструктор, в который передаем функцию с параметром инт
        /// </summary>
        public MenuItem(string text, ActionP Act, int parameter, params MenuItem[] items)
        {
            this.text = text;
            this.ActP = Act;
            this.selected = false;
            this.parameter = parameter;
            this.items = new List<MenuItem>(items) ;
        }

        public MenuItem(string text, Action Act, params MenuItem[] items)
        {
            this.text = text;
            this.Act = Act;
            this.selected = false;
            this.items = new List<MenuItem>(items);
        }

        public MenuItem(string text, params MenuItem[] items)
        {
            this.text = text;
            this.selected = false;
            this.items = new List<MenuItem>(items);
        }

        public void Do()
        {
            if (Act != null) Act();
            if (ActP != null) ActP(parameter);
        }
    }
}
