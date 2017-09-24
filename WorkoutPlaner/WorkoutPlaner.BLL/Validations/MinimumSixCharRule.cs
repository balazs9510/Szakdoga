using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlaner.BLL.Validations
{
    public class MinimumSixCharRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public virtual bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return str.Length>5;
        }
    }
}
