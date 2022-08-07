using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace menu_base
{
    public class CONFIG
    {
        public class MENU
        {
            public class JSON
            {
                public static String MENU = "json/menu.json";
                public static String STRUCTS = "json/structs.json";
            }
            public class TITLE
            {
                public class TEXT
                {
                    public static String _TEXT = "GUIZZ";
                    public class FONT
                    {
                        public static int SIZE = 50; //px
                        public class COLOR
                        {
                            public static bool BACKROUND_TRANSPARENT = true;
                            public static Color BACKROUND = Color.FromArgb(255, 255, 0, 0);
                            public static Color FORE = Color.FromArgb(255, 255, 0, 0);
                        }
                    }
                }
                public class PANEL
                {
                    public static int HEIGHT = 80; //px
                    public class FONT
                    {
                        public class COLOR
                        {
                            public static bool BACKGROUND_TRANSPARENT = true;
                            public static Color BACKGROUND = Color.FromArgb(255, 255, 255, 255);
                        }
                    }
                }
            }
            public class PROCESS
            {
                public static string NAME = "EXEMPLE"; //no .exe
            }
            public class DESIGN
            {
                public static double OPACITY = 0.75;
                public static bool TOP_MOST = true;
                public static int TOP = 50;
                public static int LEFT = 1000;
                public static int MOOVE_I_X = 10;
                public static int MOOVE_I_Y = 6;
                public class SIZE
                {
                    public static int X = 300;
                    public static int Y = 600;
                }
                public class COLOR
                {
                    public static Color BACKROUND = Color.Black;
                    public static Color TRANSPARENCY_KEY = Color.White;
                }
            }
            public class CONSOLE
            {
                public static bool ENABLE = false;
            }
            public class KEYBOARD
            {
                public static Keys OPEN = Keys.F5;
                public static Keys DOWN = Keys.NumPad2;
                public static Keys UP = Keys.NumPad8;
                public static Keys CONFIRM = Keys.NumPad5;
                public static Keys RETURN = Keys.NumPad0;
                public static Keys DECREMENT = Keys.NumPad4;
                public static Keys INCREMENT = Keys.NumPad6;
                public static Keys MOOVE_X_L = Keys.NumPad1;
                public static Keys MOOVE_X_R = Keys.NumPad3;
                public static Keys MOOVE_Y_B = Keys.NumPad7;
                public static Keys MOOVE_Y_U = Keys.NumPad9;

                public static int REFRESH = 10; //ms
            }
        }
    }
}
