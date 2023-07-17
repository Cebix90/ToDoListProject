using Moq;
using ToDoList.Core;
using ToDoList.Core.Helpers;
using ToDoList.Core.ViewModels.Pages;
using ToDoList.Database;
using ToDoList.Database.Entities;

namespace ToDoList.Tests.Core.ViewModels.Pages
{
    [TestClass]
    public class LoginPageViewModelTests
    {
        [TestInitialize]
        public void InitializeDatabase()
        {
            DatabaseLocator.Database = new ToDoListDbContext();
            DatabaseLocator.Database.Database.EnsureCreated();

            // Check if the user already exists in the Users table
            var existingUser = DatabaseLocator.Database.Users.FirstOrDefault(u => u.Email == "test@example.com");

            if (existingUser == null)
            {
                // Add a test user to the Users table
                var user = new User
                {
                    Email = "test@example.com",
                    Password = "password",
                    NickName = "testuser",
                    Country = "United States"
                };
                DatabaseLocator.Database.Users.Add(user);
                DatabaseLocator.Database.SaveChanges();
            }
        }


        [TestMethod]
        public void LoginCommand_Executed_SuccessfulLogin()
        {
            // Arrange
            DatabaseLocator.Database = new ToDoListDbContext();
            DatabaseLocator.Database.Database.EnsureCreated();

            var authenticationCommandsMock = new Mock<AuthenticationCommands>(MockBehavior.Strict, DatabaseLocator.Database);

            authenticationCommandsMock.Setup(ac => ac.GetUserIdByEmail(It.IsAny<string>())).Returns(Guid.NewGuid());
            authenticationCommandsMock.Setup(ac => ac.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var viewModel = new LoginPageViewModel(DatabaseLocator.Database)
            {
                Email = "test@example.com",
                Password = "password",
                AuthenticationCommands = authenticationCommandsMock.Object
            };

            bool loginSuccessEventRaised = false;
            viewModel.LoginSuccess += (sender, args) => loginSuccessEventRaised = true;

            // Act
            viewModel.LoginCommand.Execute(null);

            // Assert
            Assert.IsTrue(loginSuccessEventRaised);
        }

        [TestMethod]
        public void LoginCommand_Executed_FailedLogin()
        {
            // Arrange
            var authenticationCommandsMock = new Mock<AuthenticationCommands>(MockBehavior.Strict, new ToDoListDbContext());

            authenticationCommandsMock.Setup(ac => ac.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            authenticationCommandsMock.Setup(ac => ac.GetUserIdByEmail(It.IsAny<string>())).Returns(Guid.Empty);
            var viewModel = new LoginPageViewModel(new ToDoListDbContext())
            {
                Email = "test@test.pl",
                Password = "wrongpassword",
                AuthenticationCommands = authenticationCommandsMock.Object
            };

            bool loginFailedEventRaised = false;
            viewModel.LoginFailed += (sender, args) => loginFailedEventRaised = true;

            // Act
            viewModel.LoginCommand.Execute(null);

            // Assert
            Assert.IsTrue(loginFailedEventRaised);
        }

        [TestMethod]
        public void LoginCommand_Executed_RaisesLoginSuccessWithCorrectUserId()
        {
            // Arrange
            var dbContextMock = new Mock<ToDoListDbContext>();
            var authCommandsMock = new Mock<AuthenticationCommands>(dbContextMock.Object);
            var loggedInUserId = Guid.NewGuid();
            authCommandsMock.Setup(a => a.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            authCommandsMock.Setup(a => a.GetUserIdByEmail(It.IsAny<string>())).Returns(loggedInUserId);

            var viewModel = new LoginPageViewModel(dbContextMock.Object)
            {
                Email = "test@example.com",
                Password = "password",
                AuthenticationCommands = authCommandsMock.Object // Ustaw właściwość AuthenticationCommands na zmockowany obiekt
            };

            bool loginSuccessEventRaised = false;
            Guid raisedUserId = Guid.Empty;
            viewModel.LoginSuccess += (sender, args) =>
            {
                loginSuccessEventRaised = true;
                raisedUserId = viewModel.LoggedInUserId;
            };

            // Act
            viewModel.LoginCommand.Execute(null);

            // Assert
            Assert.IsTrue(loginSuccessEventRaised);
            Assert.AreEqual(loggedInUserId, raisedUserId);
        }

        [TestMethod]
        public void SignUpCommand_Executed_RaisesSignUpRequestedEvent()
        {
            // Arrange
            var dbContextMock = new Mock<ToDoListDbContext>();
            var viewModel = new LoginPageViewModel(dbContextMock.Object);
            bool signUpRequestedEventRaised = false;
            viewModel.SignUpRequested += (sender, args) => signUpRequestedEventRaised = true;

            // Act
            viewModel.SignUpCommand.Execute(null);

            // Assert
            Assert.IsTrue(signUpRequestedEventRaised);
        }
    }
}
