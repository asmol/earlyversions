using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Game.Helpers;

namespace Utils.Commands
{
    /// <summary>
    /// ядро для всех. Описание смотрим в readme.txt 
    /// </summary>
    public abstract class Command
    {
        public static readonly List<Type> COMMAND_LIST = new List<Type>{
            typeof(ComEmpty),
            typeof(ComConnect),
            typeof(ComConnectionResult), 
           /* typeof(ComAddPlayer),
            typeof(ComRemovePlayer)*/
        };

        #region абстрактные методы
        /// <summary>
        /// первым должен быть закодирован base.type, затем - остальные поля в порядке считывания
        /// </summary>
        public abstract List<byte> GetByteData(); 
        /// <summary>
        /// передаются сразу смысловые данные, без base.type вначале(он будет автоматически считан ранее).
        /// </summary>
        protected abstract void SetFields(ref List<byte> data);
        #endregion

        #region общие для всех комманд поля
        /// <summary>
        /// номер команды в порядке ее создания
        /// </summary>
        public readonly long id;
        /// <summary>
        /// тип команды, который будет первым в ByteData
        /// </summary>
        public readonly int type;
        /// <summary>
        /// Переменная для сервера. Показывает, от какого клиента пришло сообщение. 
        /// Когда создаем команду на сервере, нужно присвоить сюда id получателя
        /// </summary>
        public int clientID=-1;
        /// <summary>
        /// клиент заполняет это поле true в момент получения от сервера подтверждения, что сообщение доставлено.
        /// Используется только на стороне клиента
        /// </summary>
        public bool confirmed = false;
        #endregion

        #region конструктор
        public Command()
        {
            this.id = CurID++;
            this.type = GetNumberByType(this.GetType());
        }
        #endregion

        #region Служебные методы и поля - про них не обязательно знать, чтобы пользоваться
        /// <summary>
        /// Читает тип команды и создает его через пустой конструктор. Затем вызывает SetFields.
        /// </summary>
        /// <returns>возвращает конкретную команду, унаследованную от Command</returns>
        public static Command CreateConcrete(ref List<byte> data)
        {
            Command r; Type t;
            int typeInt = HEncoder.GetInt(ref data);
            try
            {
                t = COMMAND_LIST[typeInt];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Список команд Command.COMMAND_LIST не содержит команды с номером " + typeInt.ToString());
            }
            try
            {
                r = Activator.CreateInstance(t) as Command;
            }
            catch (MissingMethodException)
            {
                throw new Exception("Все команды должны содержать конструктор без параметров, для создания по массиву байт");
            }
            r.SetFields(ref data);
            return r;
        }

        static long CurID = 0;

        static int GetNumberByType(Type t)
        {
            int r = COMMAND_LIST.FindIndex((a) => a.Equals(t));
            if (r == -1) throw new Exception("Вероятно, вы забыли добавить команду " + t.Name + " в Command.COMMAND_LIST");
            return r;
        }
        #endregion
    }
}
