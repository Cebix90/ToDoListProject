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
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private void Login()
        {
            if (_authcommands.AuthenticateUser(Email, Password))
            {
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                LoginFailed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
