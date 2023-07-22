using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.Models.Base;
using ToDoList.Database;
using BC = BCrypt.Net.BCrypt;

namespace ToDoList.Core.ViewModels.Pages
{
    public class LoginPageViewModel : BaseViewModel
    {
        public AuthenticationCommands AuthenticationCommands { get; set; }
        /// <summary>
        /// Event raised when the login is successful.
        /// </summary>
        public event EventHandler LoginSuccess;

        /// <summary>
        /// Event raised when the login fails.
        /// </summary>
        public event EventHandler LoginFailed;

        /// <summary>
        /// Event raised when a sign-up is requested.
        /// </summary>
        public event EventHandler SignUpRequested;

        /// <summary>
        /// Gets the ID of the logged-in user.
        /// </summary>
        public Guid LoggedInUserId { get; private set; }

        private string _email;
        private string _password;
        /*private string _maskedPassword;*/
        private readonly ToDoListDbContext _context;

        /// <summary>
        /// Command for logging in.
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// Command for signing up.
        /// </summary>
        public ICommand SignUpCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPageViewModel"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public LoginPageViewModel(ToDoListDbContext context)
        {
            _context = context;
            SignUpCommand = new RelayCommand(NavigateToSignUp);
            LoginCommand = new RelayCommand(Login);
            AuthenticationCommands = new AuthenticationCommands(_context);
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary
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

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
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

        /// <summary>
        /// Performs the login process by authenticating the user with the provided email and password.
        /// </summary>
        private void Login()
        {
            Guid userId = AuthenticationCommands.GetUserIdByEmail(Email);
            if (AuthenticationCommands.AuthenticateUser(Email, Password))
            {
                LoggedInUserId = userId;
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                LoginFailed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Navigates to the sign-up page.
        /// </summary>
        private void NavigateToSignUp()
        {
            SignUpRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
