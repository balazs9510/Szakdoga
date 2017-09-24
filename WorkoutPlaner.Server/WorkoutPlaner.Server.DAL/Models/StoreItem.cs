using System;
using System.Collections.Generic;
using System.Text;
using WorkoutPlaner.Server.DAL.Enumerations;

namespace WorkoutPlaner.Server.DAL.Models
{
    public class StoreItem 
    {
        public string ServerId { get; set; }
        public StoreItemState State { get; set; }
    }
}
