using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlaner.Sync
{
    public class SyncMessage
    {
        public bool IsSucessfull { get; set; }
        public string Result { get; set; }
        public SyncMessage()
        {
            IsSucessfull = false;
            Result = string.Empty;
        }
    }
}
