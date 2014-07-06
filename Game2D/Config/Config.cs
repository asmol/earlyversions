using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2D.Opengl;

namespace Game2D
{
    //вынесены для краткости кода
    //Доступные спрайты. end - чтобы можно было легко пробежать по всем
    public enum ESprite { background, menuback, shell0,tank0, tank1, tank2,explosion, end }
    public enum EFont {  orange, fiol,  green,lilac, end }
    
    //действия, которые поддерживает клавиатура. Должны быть привязаны конкретные кнопки в конструкторе
    public enum EKeyboardAction { Fire, Esc, Enter, 
        Left, Right, Up, Down, 
        D0, D1, D2, D3, D4, D5, D6, D7, D8, D9,
        end };

    class Config
    {
        #region nested sprite config class
        public class SpriteConfig
        {
            public string file;
            public int horFrames, vertFrames;
            /// <summary>
            /// имя относительно екзешника, количество кадров по горизонтали и вертикали в файле
            /// </summary>
            public SpriteConfig(string file, int horFrames, int vertFrames)
            {
                this.file = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, file);
                this.horFrames = horFrames;
                this.vertFrames = vertFrames;
            }
            public SpriteConfig(string file) : this(file, 1, 1) { }
        }
        #endregion

        //Сюда кидаем и спрайты, и фонты. Ключ должен быть EFont.ToString() или ESprite.ToString()
        static public readonly Dictionary<string, SpriteConfig> Sprites = new Dictionary<string, SpriteConfig>();
        //сопоставили действия клавиатуры с конкретными клавишами
        static public readonly Dictionary<EKeyboardAction, byte> Keys= new Dictionary<EKeyboardAction,byte>();

        public static string WindowName = "2D Framework"; 
        public const double ScreenWidth = 133;
        public const double ScreenHeight = 100;
        public const int TimePerFrame = 20; //в миллисекундах

        public static int MaxWaitingConnectionTime = 2000;
        public static Point2 LetterSize1 = new Point2(1, 2);
        public static Point2 LetterSize2 = new Point2(2, 4);
        public static Point2 LetterSize3 = new Point2(3, 6);
        
        static Config()
        {
            LoadSpritesAuto();
            Sprites[ESprite.explosion.ToString()].horFrames = Sprites[ESprite.explosion.ToString()].vertFrames = 4;
           
            //стрелки не работают пока что
            Keys.Add(EKeyboardAction.Fire, 32);
            Keys.Add(EKeyboardAction.Esc, 27);
            Keys.Add(EKeyboardAction.Enter, 13);
            Keys.Add(EKeyboardAction.Left, 37);
            Keys.Add(EKeyboardAction.Up, 38);
            Keys.Add(EKeyboardAction.Right, 39);
            Keys.Add(EKeyboardAction.Down, 40);

            byte i = 0;
            for(EKeyboardAction a = EKeyboardAction.D0; a <= EKeyboardAction.D9; a++)
            {
                Keys.Add(a,(byte)(48+i));
                i++;
            }
        }

        public const string FontLetters = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя!@#$%^&*()_+=,./?<>[]\{}|1234567890~`‘“№→-";

        //потом подправить кадры элементарно, если анимация
        static void LoadSpritesAuto()
        {
            for (ESprite i = (ESprite)0; i != ESprite.end; i++)
            {
                Sprites.Add(i.ToString(), new SpriteConfig("textures//" + i.ToString() + ".png", 1, 1));
            }
            for (EFont i = (EFont)0; i != EFont.end; i++)
            {
                Sprites.Add(i.ToString(), new SpriteConfig("fonts//" + i.ToString() + ".png", 16, 10));
            }
        }

    }
}
