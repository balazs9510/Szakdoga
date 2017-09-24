using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlaner.BLL.Validations;

namespace WorkoutPlaner.BLL.Model
{
    public class LoginModel
    {
        public LoginModel()
        {
            Email = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
        }
        public ValidatableObject<string> Email { get; set; }
        public ValidatableObject<string> Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
