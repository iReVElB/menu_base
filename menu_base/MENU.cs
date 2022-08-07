using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.InteropServices;
using System.Threading;

namespace menu_base
{
    public partial class MENU : Form
    {

        private MENU_MANAGER MENU_MANAGER = null;

        public MENU()
        {
            this.Visible = false;
            if (CONFIG.MENU.CONSOLE.ENABLE) { AllocConsole(); }
            InitializeComponent();
            REFRESH_CONFIG();
        }

        private void REFRESH_CONFIG()
        {
            Console.WriteLine("REFRESH CONFIG...");
            //menu
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(CONFIG.MENU.DESIGN.SIZE.X, CONFIG.MENU.DESIGN.SIZE.Y);
            this.BackColor = CONFIG.MENU.DESIGN.COLOR.BACKROUND;
            this.TransparencyKey = CONFIG.MENU.DESIGN.COLOR.TRANSPARENCY_KEY;
            this.Opacity = CONFIG.MENU.DESIGN.OPACITY;
            this.TopMost = CONFIG.MENU.DESIGN.TOP_MOST;
            this.Hide();

            this.Left = CONFIG.MENU.DESIGN.LEFT;
            this.Top = CONFIG.MENU.DESIGN.TOP;

            int STYLE = UTILS.GetWindowLong(this.Handle, -20);
            UTILS.SetWindowLong(this.Handle, -20, STYLE | 0x8000 | 0x20);

            //title
            label_menu_title.Text = CONFIG.MENU.TITLE.TEXT._TEXT;
            label_menu_title.Font = new Font("Arial", CONFIG.MENU.TITLE.TEXT.FONT.SIZE);
            if (!CONFIG.MENU.TITLE.TEXT.FONT.COLOR.BACKROUND_TRANSPARENT) { label_menu_title.BackColor = CONFIG.MENU.TITLE.TEXT.FONT.COLOR.BACKROUND; }
            label_menu_title.ForeColor = CONFIG.MENU.TITLE.TEXT.FONT.COLOR.FORE;

            panel_title.Size = new Size(panel_title.Size.Width, CONFIG.MENU.TITLE.PANEL.HEIGHT);
            if (!CONFIG.MENU.TITLE.PANEL.FONT.COLOR.BACKGROUND_TRANSPARENT) { panel_title.BackColor = CONFIG.MENU.TITLE.PANEL.FONT.COLOR.BACKGROUND; }
            Console.WriteLine("CONFIG RESRESH.");
            LOAD_MENU();
        }

        Thread RAINBOW = null;

        private void LOAD_MENU()
        {
            Console.WriteLine("LOAD MENU...");
            MENU_MANAGER = new MENU_MANAGER(this, panel_menu_view, label_selection, label_option_name);
            MENU_MANAGER.LOAD_JSON(CONFIG.MENU.JSON.MENU);
            Console.WriteLine("MENU LOADED.");
            if(RAINBOW == null)
            {
                RAINBOW = new Thread(RainbowColor);
            }
            RAINBOW.Start();
        }

        private void menu_Load(object sender, EventArgs e)
        {
            
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        PaintEventArgs paintEventArgsMenuBorder = null;
        Color rainbow = new Color();
        private void panel_menu_Paint(object sender, PaintEventArgs e)
        {
            Padding padding = new Padding();
            padding.All = 1;
            panel_menu.Padding = padding;
            paintEventArgsMenuBorder = e;
            ControlPaint.DrawBorder(e.Graphics, panel_menu.ClientRectangle,
            rainbow, 5, ButtonBorderStyle.Solid, // left
            rainbow, 5, ButtonBorderStyle.Solid, // top
            rainbow, 5, ButtonBorderStyle.Solid, // right
            rainbow, 5, ButtonBorderStyle.Solid);// bottom
        }

        private void MENU_Paint(object sender, PaintEventArgs e)
        {

        }

        public void RainbowColor()
        {
            int r = 255;
            int g = 0;
            int b = 0;
            Boolean rv = false;

            while (true)
            {
                if (!rv)
                {
                    if (b == 255)
                    {
                        if (r == 0)
                        {

                            if (g == 255)
                            {
                                rv = true;
                            }
                            else
                            {
                                g++;
                            }

                        }
                        else
                        {
                            r--;
                        }
                    }
                    else
                    {
                        b++;
                    }
                }
                else
                {
                    if (b == 0)
                    {
                        if (r == 255)
                        {

                            if (g == 0)
                            {
                                rv = false;
                            }
                            else
                            {
                                g--;
                            }
                        }
                        else
                        {
                            r++;
                        }
                    }
                    else
                    {
                        b--;
                    }
                }
                rainbow = Color.FromArgb(255, r, g, b);
                CHANGE_ALL_LABEL_COLOR(rainbow);
                CHANGE_BORDER_COLOR(rainbow);
                Thread.Sleep(50);
            }
        }

        private void CHANGE_BORDER_COLOR(Color rainbow)
        {
            Invoke(new MethodInvoker(delegate { panel_menu.Refresh(); }));
        }

        public void CHANGE_ALL_LABEL_COLOR(Color c)
        {
            Invoke(new MethodInvoker(delegate {
                label_menu_title.ForeColor = rainbow;
                label_option_name.ForeColor = rainbow;
                label_selection.ForeColor = rainbow;
            }));
        }

        public Color GET_RAINBOW_COLOR()
        {
            return rainbow;
        }
    }
}
