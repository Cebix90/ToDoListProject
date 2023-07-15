using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.Models.Base;
using ToDoList.Database;

namespace ToDoList.Core.ViewModels.Pages
{
    public class LoginPageViewModel : BaseViewModel
    {
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
        private string _maskedPassword;
        private readonly ToDoListDbContext _context;
        private readonly AuthenticationCommands _authcommands;

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
            _authcommands = new AuthenticationCommands(_context);
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
        /// Gets or sets the masked password.
        /// </summary>
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
            Guid userId = _authcommands.GetUserIdByEmail(Email);
            if (_authcommands.AuthenticateUser(Email, Password))
            {
                LoggedInUserId = userId;
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                LoginFailed?.Invoke(this, EventArgs.Empty);
            }
        }

        private string HashPassword(string password)
        {
            if (password == null)
            {
                return null;
            }

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return System.Convert.ToBase64String(hashedBytes);
            }
        }

        private void NavigateToSignUp()
        {
            SignUpRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
