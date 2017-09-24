using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlaner.EventArgs
{
    public class CheckBoxEventArgs : System.EventArgs
    {
        public CheckBoxEventArgs(bool check)
        {
            Checked = check;
        }
        public bool Checked { get; set; }
    }
}
