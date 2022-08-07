using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace menu_base
{
    public class MENU_MANAGER
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private Thread THREAD_KEYBOARD_EVENTS = null;
        MENU MENU = null;
        Panel _panel_menu_view = null;
        Label _label_selection = null;
        Label _label_option_name = null;
        //List<MENU_VIEW.Option> ALL_OPTIONS = null;
        MENU_VIEW.Option P_OPTION = null;
        MENU_VIEW.Option LAST_OPTION = null;

        List<MENU_VIEW.Option> options = new List<MENU_VIEW.Option>();
        Dictionary<string, object> values = new Dictionary<string, object>();

        int SELECTION = 0;
        int MAX_SELECTION = 0;

        public MENU_MANAGER(MENU m, Panel panel_menu_view, Label label_selection, Label label_option_name)
        {
            if(MENU == null)
            {
                MENU = m;
            }
            if(_panel_menu_view == null)
            {
                _panel_menu_view = panel_menu_view;
            }
            _label_option_name = label_option_name;
            _label_selection = label_selection;
            if(THREAD_KEYBOARD_EVENTS == null)
            {
                THREAD_KEYBOARD_EVENTS = new Thread(KEYBOARD_EVENTS);
            }
        }

        private void KEYBOARD_EVENTS()
        {
            int REFRESH = CONFIG.MENU.KEYBOARD.REFRESH;
            SHOW_INFO();
            while (true)
            {
                Thread.Sleep(REFRESH);
                if (GET_KEY_PRESSED(CONFIG.MENU.KEYBOARD.OPEN))
                {
                    Console.WriteLine("OPEN");
                    SET_MENU_VISIBLE(!MENU.Visible);
                }
                if (GET_KEY_PRESSED(CONFIG.MENU.KEYBOARD.UP))//c inverse
                {
                    Console.WriteLine("DOWN");
                    if ((SELECTION - 1) < 0) { SELECTION = MAX_SELECTION; } else { SELECTION--; }
                    SHOW_INFO();
                    UPDATE_LABEL_COLOR();
                }
                if (GET_KEY_PRESSED(CONFIG.MENU.KEYBOARD.DOWN))
                {
                    Console.WriteLine("UP");
                    if ((SELECTION + 1) > MAX_SELECTION) { SELECTION = 0; } else { SELECTION++; }
                    SHOW_INFO();
                    UPDATE_LABEL_COLOR();
                }
                if (GET_KEY_PRESSED(CONFIG.MENU.KEYBOARD.CONFIRM))
                {
                    Console.WriteLine("CONFIRM");
                    if (options.Count == 0)
                    {
                        if (MENU_VIEW.Option.GET_TYPE(MENU_VIEW.OPTIONS[SELECTION].type) != MENU_VIEW.OPTION_TYPE.VOID && MENU_VIEW.Option.GET_TYPE(MENU_VIEW.OPTIONS[SELECTION].type) != MENU_VIEW.OPTION_TYPE.BOOL && MENU_VIEW.Option.GET_TYPE(MENU_VIEW.OPTIONS[SELECTION].type) != MENU_VIEW.OPTION_TYPE.INT && MENU_VIEW.Option.GET_TYPE(MENU_VIEW.OPTIONS[SELECTION].type) != MENU_VIEW.OPTION_TYPE.FLOAT)
                        {
                            if (MENU_VIEW.OPTIONS[SELECTION].options != null)
                            {
                                options.Add(MENU_VIEW.OPTIONS[SELECTION]);
                                SHOW_OPTIONS(options[options.Count - 1]);
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (MENU_VIEW.Option.GET_TYPE(options[options.Count - 1].options[SELECTION].type) != MENU_VIEW.OPTION_TYPE.VOID && MENU_VIEW.Option.GET_TYPE(options[options.Count - 1].options[SELECTION].type) != MENU_VIEW.OPTION_TYPE.BOOL && MENU_VIEW.Option.GET_TYPE(options[options.Count - 1].options[SELECTION].type) != MENU_VIEW.OPTION_TYPE.INT && MENU_VIEW.Option.GET_TYPE(options[options.Count - 1].options[SELECTION].type) != MENU_VIEW.OPTION_TYPE.FLOAT)
                        {
                            if (options[options.Count - 1].options[SELECTION].options != null)
                            {
                                options.Add(options[options.Count - 1].options[SELECTION]);
                                SHOW_OPTIONS(options[options.Count - 1]);
                            }
                        }
                        else
                        {

                        }
                    }


                }
                if (GET_KEY_PRESSED(CONFIG.MENU.KEYBOARD.RETURN))
                {
                    Console.WriteLine("RETURN");
                    if (options.Count > 0)
                    {
                        options.RemoveAt(options.Count - 1);
                        if (options.Count > 0)
                        {
                            SHOW_OPTIONS(options[options.Count - 1]);
                        }
                        else
                        {
                            SHOW_MAIN_MENU();
                        }
                    }
                    else
                    {
                        SHOW_MAIN_MENU();
                    }
                }
                if (GET_KEY_PRESSED(CONFIG.MENU.KEYBOARD.INCREMENT))
                {
                    Console.WriteLine("INCREMENT");
                    if (options.Count > 0)
                    {
                        MENU_VIEW.Option o = options[options.Count - 1].options[SELECTION];
                        MENU_VIEW.OPTION_TYPE ot = MENU_VIEW.Option.GET_TYPE(o.type);
                        if (o.i != null)
                        {
                            if (values.ContainsKey(o.id))
                            {
                                increment(o, ot);
                            }
                            else
                            {
                                values.Add(o.id, TYPE_TO_OBJECT(o));
                                increment(o, ot);
                            }
                        }
                    }
                    void increment(MENU_VIEW.Option o, MENU_VIEW.OPTION_TYPE ot)
                    {
                        if (ot != MENU_VIEW.OPTION_TYPE.VOID && ot != MENU_VIEW.OPTION_TYPE.OPTION && ot != MENU_VIEW.OPTION_TYPE.BOOL)
                        {
                            if (ot == MENU_VIEW.OPTION_TYPE.INT)
                            {
                                if ((int)values[o.id] + (int)o.i.inc <= o.i.max)
                                {
                                    values[o.id] = (int)values[o.id] + (int)o.i.inc;
                                }
                            }
                            if (ot == MENU_VIEW.OPTION_TYPE.FLOAT)
                            {
                                if ((float)values[o.id] + (float)o.i.inc <= o.i.max)
                                {
                                    values[o.id] = (float)values[o.id] + (float)o.i.inc;
                                }
                            }
                            Label l = (Label)_panel_menu_view.Controls.Find(o.id, false).First();
                            MENU.Invoke(new MethodInvoker(delegate { l.Text = GET_CURRENT_LABEL_TEXT(o); }));
                        }
                    }

                }
                if (GET_KEY_PRESSED(CONFIG.MENU.KEYBOARD.DECREMENT))
                {
                    Console.WriteLine("DECREMENT");
                    if (options.Count > 0)
                    {
                        MENU_VIEW.Option o = options[options.Count - 1].options[SELECTION];
                        MENU_VIEW.OPTION_TYPE ot = MENU_VIEW.Option.GET_TYPE(o.type);
                        if (o.i != null)
                        {
                            if (values.ContainsKey(o.id))
                            {
                                decrement(o, ot);
                            }
                            else
                            {
                                values.Add(o.id, TYPE_TO_OBJECT(o));
                                decrement(o, ot);
                            }
                        }
                    }
                    void decrement(MENU_VIEW.Option o, MENU_VIEW.OPTION_TYPE ot)
                    {
                        if (ot != MENU_VIEW.OPTION_TYPE.VOID && ot != MENU_VIEW.OPTION_TYPE.OPTION && ot != MENU_VIEW.OPTION_TYPE.BOOL)
                        {
                            if (ot == MENU_VIEW.OPTION_TYPE.INT)
                            {
                                if ((int)values[o.id] - o.i.inc >= o.i.min)
                                {
                                    values[o.id] = (int)values[o.id] - o.i.inc;
                                }
                            }
                            if (ot == MENU_VIEW.OPTION_TYPE.FLOAT)
                            {
                                if ((float)values[o.id] - o.i.inc >= o.i.min)
                                {
                                    values[o.id] = (float)values[o.id] - o.i.inc;
                                }
                            }
                            Label l = (Label)_panel_menu_view.Controls.Find(o.id, false).First();
                            MENU.Invoke(new MethodInvoker(delegate { l.Text = GET_CURRENT_LABEL_TEXT(o); }));
                        }
                    }
                }
                if (GET_KEY_PRESSED(CONFIG.MENU.KEYBOARD.MOOVE_X_L))
                {
                    //if (MENU.Location.X - CONFIG.MENU.DESIGN.MOOVE_I_X > 0)
                    //{
                        MENU.Invoke(new MethodInvoker(delegate { MENU.Location = new Point(MENU.Location.X - CONFIG.MENU.DESIGN.MOOVE_I_X, MENU.Location.Y); }));
                        SHOW_MENU_LOCATION();
                    //}
                }
                if (GET_KEY_PRESSED(CONFIG.MENU.KEYBOARD.MOOVE_X_R))
                {
                    MENU.Invoke(new MethodInvoker(delegate { MENU.Location = new Point(MENU.Location.X + CONFIG.MENU.DESIGN.MOOVE_I_X, MENU.Location.Y); }));
                    SHOW_MENU_LOCATION();
                }
                if (GET_KEY_PRESSED(CONFIG.MENU.KEYBOARD.MOOVE_Y_B))
                {
                        MENU.Invoke(new MethodInvoker(delegate { MENU.Location = new Point(MENU.Location.X, MENU.Location.Y + CONFIG.MENU.DESIGN.MOOVE_I_Y); }));
                        SHOW_MENU_LOCATION();

                }
                if (GET_KEY_PRESSED(CONFIG.MENU.KEYBOARD.MOOVE_Y_U))
                {
                    //if (MENU.Location.Y - CONFIG.MENU.DESIGN.MOOVE_I_Y > 0)
                    //{
                        MENU.Invoke(new MethodInvoker(delegate { MENU.Location = new Point(MENU.Location.X, MENU.Location.Y - CONFIG.MENU.DESIGN.MOOVE_I_Y); }));
                        SHOW_MENU_LOCATION();
                    //}
                }
                void SHOW_MENU_LOCATION()
                {
                    Console.WriteLine(MENU.Location.ToString());
                }
            }
        }

        //Console.WriteLine(values[o.id].ToString());
        //Console.WriteLine("values count: " + values.Count); //SI IL INCREMENT NE PAS DEPASSER LE MAX, ET SI IL -- DE PAS DEPASSER LE MIN ET APRES REFRESH LE LABEL ET LE REFRESH QUAND IL RENTRE DANS UNE OPTION.

        public void SHOW_INFO()
        {
            if(options.Count > 0)
            {
                Console.WriteLine("[" + SELECTION + "/" + MAX_SELECTION + "] " + options[options.Count - 1].options[SELECTION].id.ToString());
            } else
            {
                Console.WriteLine("[" + SELECTION + "/" + MAX_SELECTION + "] " + MENU_VIEW.OPTIONS[SELECTION].id.ToString());
            }
        }

        public void UPDATE_LABEL_COLOR()
        {
            if ((SELECTION - 1) < 0) { _panel_menu_view.Controls[MAX_SELECTION].ForeColor = Color.Yellow; } else { _panel_menu_view.Controls[SELECTION - 1].ForeColor = Color.Yellow; }
            if ((SELECTION + 1) > MAX_SELECTION) { _panel_menu_view.Controls[0].ForeColor = Color.Yellow; } else { _panel_menu_view.Controls[SELECTION + 1].ForeColor = Color.Yellow; }
            _panel_menu_view.Controls[SELECTION].ForeColor = MENU.GET_RAINBOW_COLOR();
            MENU.Invoke(new MethodInvoker(delegate { _label_selection.Text = SELECTION + "/" + MAX_SELECTION; }));
        }

        private bool GET_KEY_PRESSED(Keys KEY)
        {
            return (GetAsyncKeyState(KEY) == -32767) ;
        }

        private void SET_MENU_VISIBLE(Boolean VISIBLE)
        {
            MENU.Invoke(new MethodInvoker(delegate { MENU.Visible = VISIBLE; }));
        }

        public void SHOW_MAIN_MENU()
        {
            MENU.Invoke(new MethodInvoker(delegate { _label_option_name.Text = "Main"; }));
            MAX_SELECTION = MENU_VIEW.OPTIONS.Count - 1;
            CLEAR_CONTROLS();
            foreach(MENU_VIEW.Option o in MENU_VIEW.OPTIONS)
            {
                ADD_LABEL(o);
            }
            UPDATE_LABEL_COLOR();
        }

        public void SHOW_OPTIONS(MENU_VIEW.Option option)
        {
            if (options.Count > 0)
            {
                MENU.Invoke(new MethodInvoker(delegate { _label_option_name.Text = options[options.Count - 1].name.ToString(); }));
            }
            if (option.options != null)
            {
                P_OPTION = option;
                int index = SELECTION;
                SELECTION = 0;
                MAX_SELECTION = option.options.Count - 1;
                CLEAR_CONTROLS();
                foreach (MENU_VIEW.Option o in option.options)
                {
                    if (values.ContainsKey(o.id))
                    {
                        ADD_LABEL(o, GET_CURRENT_LABEL_TEXT(o));
                    } else
                    {
                        ADD_LABEL(o);
                    }
                }
                UPDATE_LABEL_COLOR();
            }
        }

        public void SHOW_OPTIONS_TEST(MENU_VIEW.Option option)
        {
            CLEAR_CONTROLS();
            foreach (MENU_VIEW.Option o in option.options)
            {
                ADD_LABEL(o);
            }
        }

        public string GET_CURRENT_LABEL_TEXT(MENU_VIEW.Option o)
        {
            return o.name.ToString() + " [" + values[o.id].ToString() + "/" + o.i.max + "]";
        }

        public void ADD_LABEL(MENU_VIEW.Option o, String text = null)
        {
            Label label = new Label();
            label.Name = o.id;
            if(text == null) { label.Text = GET_LABEL_TEXT(o); } else { label.Text = text; }
            label.ForeColor = Color.Yellow;
            label.Font = new Font(label.Font.FontFamily, 15);
            label.AutoSize = false;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Dock = DockStyle.Bottom;
            label.Height = label.Height + 5;
            label.Visible = true;
            MENU.Invoke(new MethodInvoker(delegate
            {
                _panel_menu_view.Controls.Add(label);
            }));
        }

        public String GET_LABEL_TEXT(MENU_VIEW.Option o)
        {
            String NAME_BASE = o.name;
            String NAME = "??? " + NAME_BASE;
            MENU_VIEW.OPTION_TYPE ot;
            if(o != null)
            {
                ot = MENU_VIEW.Option.GET_TYPE(o.type);
            } else
            {
                ot = MENU_VIEW.OPTION_TYPE.OPTION;
            }

            switch (ot)
            {
                case MENU_VIEW.OPTION_TYPE.OPTION:
                    NAME = NAME_BASE;
                    break;
                case MENU_VIEW.OPTION_TYPE.VOID:
                    NAME = "->" + NAME_BASE;
                    break;
                case MENU_VIEW.OPTION_TYPE.BOOL:
                    NAME = NAME_BASE + " [OFF]";
                    break;
                case MENU_VIEW.OPTION_TYPE.INT:
                    NAME = NAME_BASE + " [" + o.i.min + "/" + o.i.max + "]";
                    break;
                case MENU_VIEW.OPTION_TYPE.FLOAT:
                    NAME = NAME_BASE + " [" + o.i.min + "/" + o.i.max + "]";
                    break;
            }
            return NAME;
        }

        public object TYPE_TO_OBJECT(MENU_VIEW.Option o)
        {
            MENU_VIEW.OPTION_TYPE ot;
            ot = MENU_VIEW.Option.GET_TYPE(o.type);
            object OBJ = null;
            switch (ot)
            {
                case MENU_VIEW.OPTION_TYPE.BOOL:
                    OBJ = new Boolean();
                    break;
                case MENU_VIEW.OPTION_TYPE.INT:
                    OBJ = new int();
                    break;
                case MENU_VIEW.OPTION_TYPE.FLOAT:
                    OBJ = new float();
                    break;
            }
            return OBJ;
        }

        public void CLEAR_CONTROLS()
        {
            MENU.Invoke(new MethodInvoker(delegate { _panel_menu_view.Controls.Clear(); }));
        }

        public void LOAD_JSON(String json)
        {
            Console.WriteLine("LOAD JSON MENU...");
            using (StreamReader SR = new StreamReader(json))
            {
                String JSON = SR.ReadToEnd();
                MENU_VIEW.OPTIONS = JsonConvert.DeserializeObject<List<MENU_VIEW.Option>>(JSON);
                //List<MENU_VIEW.Option> options = JsonConvert.DeserializeObject<List<MENU_VIEW.Option>>(JsonConvert.SerializeObject(MENU_VIEW.ROOT));
                //ALL_OPTIONS = options;
                Console.WriteLine("MENU_VIEW.ROOT: " + MENU_VIEW.OPTIONS.Count);
                Console.WriteLine(MENU_VIEW.OPTIONS[0].options[3].i.max);
                Console.WriteLine("\n\n");
                foreach(MENU_VIEW.Option o in MENU_VIEW.OPTIONS)
                {
                    if(o.options != null)
                    {
                        Console.WriteLine(o.id + "->");
                        SHOW_INFO(o);
                    } else
                    {
                        
                        if(o.type != null)
                        {
                            Console.WriteLine(o.id + ":" + o.type);

                        } else
                        {
                            Console.WriteLine(o.id);
                        }
                    }
                }
                Console.WriteLine("\n\n");
            }
            Console.WriteLine("JSON MENU LOADED.");
            THREAD_KEYBOARD_EVENTS.Start();
            SHOW_MAIN_MENU();
        }

        public void SHOW_INFO(MENU_VIEW.Option o)
        {
            if (o.options != null)
            {
                foreach (MENU_VIEW.Option _o in o.options)
                {
                    if (_o.options != null)
                    {
                        Console.WriteLine("->" + _o.id);
                        SHOW_INFO(_o);
                    } else
                    {
                        Console.WriteLine("    " + _o.id + ":" + _o.type);
                    }
                }
            }
        }
    }
}

