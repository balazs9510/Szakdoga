using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlaner.BLL.Validations
{
    public class IsValidPasswordRule<T> : MinimumSixCharRule<T>
    {
        public override bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value as string;
            return str.ToCharArray().Any(c => char.IsUpper(c)) && str.Length > 5;
        }
    }
}
