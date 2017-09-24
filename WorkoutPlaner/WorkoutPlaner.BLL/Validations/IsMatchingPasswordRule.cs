using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlaner.BLL.Validations
{
    public class IsMatchingPasswordRule<T> : IValidationRule<T>
    {
        public string Password;
        public string ValidationMessage { get; set; }
        public IsMatchingPasswordRule()
        {
            Password = string.Empty;
        }
        public bool Check(T value)
        {
            if (value == null)
                return false;
            var str = value as string;
            return str.Equals(Password);
        }
    }
}
