using System.Text.RegularExpressions;

namespace WorkoutPlaner.BLL.Validations
{
    public class IsValidEmailRule<T> : IValidationRule<T>
    {
        const string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return new Regex(emailRegex).IsMatch(str);
        }
    }
}
