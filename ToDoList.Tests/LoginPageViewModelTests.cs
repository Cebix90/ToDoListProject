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
        /// <summary>
        /// Initializes the database before each test by creating a new database context and ensuring its creation. 
        /// It also adds a test user to the database if it doesn't already exist.
        /// </summary>
        [TestInitialize]
        public void InitializeDatabase()
        {
            DatabaseLocator.Database = new ToDoListDbContext();
            DatabaseLocator.Database.Database.EnsureCreated();
            
            var existingUser = DatabaseLocator.Database.Users.FirstOrDefault(u => u.Email == "test@example.com");

            if (existingUser == null)
            {
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

        /// <summary>
        /// Tests the LoginCommand execution for a successful login.
        /// </summary>
        [TestMethod]
        public void LoginCommand_Executed_SuccessfulLogin()
        {
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
            
            viewModel.LoginCommand.Execute(null);
            
            Assert.IsTrue(loginSuccessEventRaised);
        }

        /// <summary>
        /// Tests the LoginCommand execution for a failed login.
        /// </summary>
        [TestMethod]
        public void LoginCommand_Executed_FailedLogin()
        {
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
            
            viewModel.LoginCommand.Execute(null);
            
            Assert.IsTrue(loginFailedEventRaised);
        }

        /// <summary>
        /// Tests the LoginCommand execution and verifies that the LoginSuccess event is raised with the correct user ID.
        /// </summary>
        [TestMethod]
        public void LoginCommand_Executed_RaisesLoginSuccessWithCorrectUserId()
        {
            var dbContextMock = new Mock<ToDoListDbContext>();
            var authCommandsMock = new Mock<AuthenticationCommands>(dbContextMock.Object);
            var loggedInUserId = Guid.NewGuid();
            authCommandsMock.Setup(a => a.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            authCommandsMock.Setup(a => a.GetUserIdByEmail(It.IsAny<string>())).Returns(loggedInUserId);

            var viewModel = new LoginPageViewModel(dbContextMock.Object)
            {
                Email = "test@example.com",
                Password = "password",
                AuthenticationCommands = authCommandsMock.Object
            };

            bool loginSuccessEventRaised = false;
            Guid raisedUserId = Guid.Empty;
            viewModel.LoginSuccess += (sender, args) =>
            {
                loginSuccessEventRaised = true;
                raisedUserId = viewModel.LoggedInUserId;
            };
            
            viewModel.LoginCommand.Execute(null);
            
            Assert.IsTrue(loginSuccessEventRaised);
            Assert.AreEqual(loggedInUserId, raisedUserId);
        }

        /// <summary>
        /// Tests the SignUpCommand execution and verifies that the SignUpRequested event is raised.
        /// </summary>
        [TestMethod]
        public void SignUpCommand_Executed_RaisesSignUpRequestedEvent()
        {
            var dbContextMock = new Mock<ToDoListDbContext>();
            var viewModel = new LoginPageViewModel(dbContextMock.Object);
            bool signUpRequestedEventRaised = false;
            viewModel.SignUpRequested += (sender, args) => signUpRequestedEventRaised = true;
            
            viewModel.SignUpCommand.Execute(null);
            
            Assert.IsTrue(signUpRequestedEventRaised);
        }
    }
}
