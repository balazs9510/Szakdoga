using WorkoutPlaner.BLL.Validations;

namespace WorkoutPlaner.BLL.Model
{
    public class RegisterModel : LoginModel
    {
        public ValidatableObject<string> ConfirmPassword { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weigth { get; set; }
        public RegisterModel() : base()
        {
            ConfirmPassword = new ValidatableObject<string>();
            Name = string.Empty;
        }
    }
}
