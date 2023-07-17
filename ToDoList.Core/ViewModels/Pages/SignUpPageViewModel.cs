using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.Models.Base;
using ToDoList.Database.Entities;

namespace ToDoList.Core.ViewModels.Pages;

public class SignUpPageViewModel : BaseViewModel
{
    /// <summary>
    /// Event raised when a login is requested.
    /// </summary>
    public event EventHandler LoginRequested;

    /// <summary>
    /// Event raised when the entered email is already taken.
    /// </summary>
    public event EventHandler EmailIsTaken;

    /// <summary>
    /// Event raised when the entered nickname is already taken.
    /// </summary>
    public event EventHandler NickNameIsTaken;

    /// <summary>
    /// Event raised when a nickname must be entered.
    /// </summary>
    public event EventHandler NickNameMustBeEntered;

    /// <summary>
    /// Event raised when the entered password is too short.
    /// </summary>
    public event EventHandler PasswordIsToShort;

    /// <summary>
    /// Event raised when the entered email is not in the correct form.
    /// </summary>
    public event EventHandler EmailIsNotInCorrectForm;

    /// <summary>
    /// Event raised when the submission is successful.
    /// </summary>
    public event EventHandler SubmitSuccessfully;

    private string _email;
    private string _password;
    private string _nickName;
    private string _country;

    /// <summary>
    /// Command for signing up a new user.
    /// </summary>
    public ICommand SignUpCommand { get; set; }

    /// <summary>
    /// Command for navigating to the login page.
    /// </summary>
    public ICommand LoginCommand { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SignUpPageViewModel"/> class.
    /// </summary>
    public SignUpPageViewModel()
    {
        SignUpCommand = new RelayCommand(AddNewUser);
        LoginCommand = new RelayCommand(NavigateToLoginPage);
    }

    /// <summary>
    /// Gets or sets the new email.
    /// </summary>
    public string NewEmail
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
                OnPropertyChanged(nameof(NewEmail));
            }
        }
    }

    /// <summary>
    /// Gets or sets the new password.
    /// </summary>
    public string NewPassword
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
                OnPropertyChanged(nameof(NewPassword));
            }
        }
    }

    /// <summary>
    /// Gets or sets the new nickname.
    /// </summary>
    public string NewNickName
    {
        get
        {
            return _nickName;
        }
        set
        {
            if (_nickName != value)
            {
                _nickName = value;
                OnPropertyChanged(nameof(NewNickName));
            }
        }
    }

    /// <summary>
    /// Gets or sets the new country.
    /// </summary>
    public string NewCountry
    {
        get
        {
            return _country;
        }
        set
        {
            if (_country != value)
            {
                _country = value;
                OnPropertyChanged(nameof(NewCountry));
            }
        }
    }

    /// <summary>
    /// Adds a new user to the database with the provided information.
    /// </summary>
    private void AddNewUser()
    {
        bool emailExists = DatabaseLocator.Database.Users.Any(u => u.Email == NewEmail);
        bool nicknameExists = DatabaseLocator.Database.Users.Any(u => u.NickName == NewNickName);

        if (emailExists)
        {
            EmailIsTaken?.Invoke(this, EventArgs.Empty);
            return;
        }

        try
        {
            var addr = new System.Net.Mail.MailAddress(NewEmail);
            int atIndex = NewEmail.IndexOf('@');
            int lastDotIndex = NewEmail.LastIndexOf('.');
            if (addr.Address != NewEmail || atIndex < 0 || lastDotIndex < atIndex)
            {
                EmailIsNotInCorrectForm?.Invoke(this, EventArgs.Empty);
                return;
            }
        }
        catch
        {
            EmailIsNotInCorrectForm?.Invoke(this, EventArgs.Empty);
            return;
        }

        if (nicknameExists)
        {
            NickNameIsTaken?.Invoke(this, EventArgs.Empty);
            return;
        }
        
        if (NewPassword == null || NewPassword.Length < 5)
        {
            PasswordIsToShort?.Invoke(this, EventArgs.Empty);
            return;
        }

        if (NewNickName == null)
        {
            NickNameMustBeEntered?.Invoke(this, EventArgs.Empty);
            return;
        }

        DatabaseLocator.Database.Users.Add(new User
        {
            Email = NewEmail,
            Password = NewPassword,
            NickName = NewNickName,
            Country = NewCountry
        });

        DatabaseLocator.Database.SaveChanges();

        SubmitSuccessfully?.Invoke(this, EventArgs.Empty);

        ClearFields();
    }

    /// <summary>
    /// Clears the fields used for user registration.
    /// </summary>
    private void ClearFields()
    {
        NewEmail = string.Empty;
        NewPassword = string.Empty;
        NewNickName = string.Empty;
        NewCountry = string.Empty;
    }

    /// <summary>
    /// Navigates to the login page.
    /// </summary>
    private void NavigateToLoginPage()
    {
        LoginRequested?.Invoke(this, EventArgs.Empty);
    }
}