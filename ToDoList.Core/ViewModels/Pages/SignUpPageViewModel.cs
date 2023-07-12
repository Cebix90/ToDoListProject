using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.Models.Base;
using ToDoList.Database.Entities;

namespace ToDoList.Core.ViewModels.Pages;

public class SignUpPageViewModel : BaseViewModel
{
    public event EventHandler LoginRequested;
    public event EventHandler EmailIsTaken;
    public event EventHandler NickNameIsTaken;
    public event EventHandler NickNameMustBeEntered;
    public event EventHandler PasswordIsToShort;
    public event EventHandler EmailIsNotInCorrectForm;

    private string _email;
    private string _password;
    private string _nickName;
    private string _country;

    public ICommand SignUpCommand { get; set; }
    public ICommand LoginCommand { get; set; }

    public SignUpPageViewModel()
    {
        SignUpCommand = new RelayCommand(AddNewUser);
        LoginCommand = new RelayCommand(NavigateToLoginPage);
    }

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

    private void AddNewUser()
    {
        bool emailExists = DatabaseLocator.Database.Users.Any(u => u.Email == NewEmail);
        bool nicknameExists = DatabaseLocator.Database.Users.Any(u => u.NickName == NewNickName);

        if (emailExists)
        {
            // Display a message to the user
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
            // Display a message to the user
            NickNameIsTaken?.Invoke(this, EventArgs.Empty);
            return;
        }

        // Check if the password is at least 5 characters long
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

        ClearFields();
    }

    private void ClearFields()
    {
        NewEmail = string.Empty;
        NewPassword = string.Empty;
        NewNickName = string.Empty;
        NewCountry = string.Empty;
    }

    private void NavigateToLoginPage()
    {
        LoginRequested?.Invoke(this, EventArgs.Empty);
    }
}