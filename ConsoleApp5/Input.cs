using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Input
    {
        private static Hashtable table = new Hashtable();

        public static bool Press(Keys key)
        {
            if (table[key] == null)
                return false;
            return (bool)table[key];
        }

        public static void ChangeState(Keys key, bool state)
        {
            table[key] = state;
        }
    }
}
