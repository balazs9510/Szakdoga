namespace WorkoutPlaner.BLL.Model
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int Age { get; set; }
        public int Weigth { get; set; }
        public string Name { get; set; }
        public RegisterViewModel()
        {
            Name = string.Empty;
        }
    }
}
