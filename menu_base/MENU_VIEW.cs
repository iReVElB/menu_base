using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace menu_base
{
    public class MENU_VIEW
    {
        public static List<Option> OPTIONS;
        public class I
        {
            public int min { get; set; }
            public int max { get; set; }
            public int inc { get; set; }

            public object value { get; set; }
        }

        public class Option
        {
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public I i { get; set; }
            public List<Option> options { get; set; }
            public static OPTION_TYPE GET_TYPE(String TEXT)
            {
                OPTION_TYPE ot;
                if (TEXT != null)
                {
                    TEXT = TEXT.ToUpper();
                    Enum.TryParse(TEXT, out ot);
                }
                else
                {
                    ot = OPTION_TYPE.OPTION;
                }

                return ot;
            }
        }

        public enum OPTION_TYPE
        {
            OPTION,
            VOID,
            BOOL,
            INT,
            FLOAT
        }
    }
}
