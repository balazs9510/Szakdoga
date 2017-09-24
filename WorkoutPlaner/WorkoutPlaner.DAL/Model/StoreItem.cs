using System;
using System.Collections.Generic;
using System.Text;
using WorkoutPlaner.Common.Enumerations;

namespace WorkoutPlaner.DAL.Model
{
    public abstract class StoreItem 
    {
        public string ServerId { get; set; }
        public StoreItemState State { get; set; }
    }
}
