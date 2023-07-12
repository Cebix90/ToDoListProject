using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.Models.Base;
using ToDoList.Database;

namespace ToDoList.Core.ViewModels.Pages
{
    public class LoginPageViewModel : BaseViewModel
    {
        public event EventHandler LoginSuccess;
        public event EventHandler LoginFailed;

        private string _email;
        private string _password;
        private string _maskedPassword;
        private readonly ToDoListDbContext _context;
        private readonly AuthenticationCommands _authcommands;

        public ICommand LoginCommand { get; set; }
        //public ICommand LoginComm { get; }

        public LoginPageViewModel(ToDoListDbContext context)
        {
            _context = context;
            //LoginComm = new CommandBlueprint(Login);
            LoginCommand = new RelayCommand(Login);
            _authcommands = new AuthenticationCommands(_context);
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string MaskedPassword
        {
            get { return _maskedPassword; }
            set
            {
                if (_maskedPassword != value)
                {
                    _maskedPassword = value;
                    OnPropertyChanged(nameof(MaskedPassword));
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    MaskedPassword = MaskPassword(_password);
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private string MaskPassword(string password)
        {
            return new string('*', password.Length);
        }

        private void Login()
        {
            string hashedPassword = HashPassword(Password);
            if (_authcommands.AuthenticateUser(Email, Password))
            {
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                LoginFailed?.Invoke(this, EventArgs.Empty);
            }
        }

        private string HashPassword(string password)
        {
            // Tutaj należy użyć odpowiedniej biblioteki lub algorytmu haszującego
            // w celu zahashowania hasła. Przykład poniżej używa SHA256.

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return System.Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
