using NUnit.Framework;
using IntroSE.Kanban.Backend;
using IntroSE.Kanban.Backend.ServiceLayer;
using Moq;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.BusinessLayer.objects;

namespace KanbanTests
{
    public class Tests
    {
        Service service;
        UserController userCtrl;
        Mock<IUser> user;
        string email;
        string password;


        [SetUp]
        public void Setup()
        {
            service = new Service();
            userCtrl = UserController.GetInstance();
            email = "example@gmail.com";
            password = "Aa12345";
            user = new Mock<IUser>();
            user.Setup(m => m.Email).Returns(email);
            userCtrl.InsertExistingUser(user.Object);
        }

        [TearDown]
        public void TearDown()
        {
            service.DeleteData();
        }

        [Test]
        public void Login_LoginValidUser_Success()
        {
            //arrange
            user.Setup(m => m.Login(password)).Returns(MFResponse<IUser>.FromValue(user.Object));

            //act
            MFResponse<IUser> res = userCtrl.Login(email, password);

            //assert
            Assert.IsFalse(res.ErrorOccured, "error occured while login legitimate user");
        }

        [TestCase("xampl@gmail.com", "Aa12345")]
        [TestCase(null, "Aa12345")]
        [TestCase("example@gmail.com", null)]
        public void Login_LoginInvalidUser_Fail(string givenEmail, string givenPassword)
        {
            //arrange
            user.Setup(m => m.Login(password)).Returns(MFResponse<IUser>.FromValue(user.Object));

            //act
            MFResponse<IUser> res = userCtrl.Login(givenEmail, givenPassword);

            //assert
            Assert.IsTrue(res.ErrorOccured, "error did not occured while login invalid user");
        }

        public void Logout_LogoutLoggedInUser_Success()
        {
            //arrange
            userCtrl.Register(email, password);

            //act
            userCtrl.Login(email, password);

            //assert
            Assert.IsFalse(userCtrl.Logout(email).ErrorOccured, "error occured while logout legitimate user");
        }
        public void Logout_LogoutLoggedOutUser_Fail()
        {
            //arrange
            string emailAdd = email;
            Password pass = new Password(password);
            userCtrl.Register(emailAdd, password);

            //act
            userCtrl.Login(emailAdd, password);
            userCtrl.Logout(emailAdd);

            //assert
            Assert.IsTrue(userCtrl.Logout(emailAdd).ErrorOccured, "error did no occured while logouting logouted user");

        }
        [Test]
        public void Resgister_RegisterExistingUser_Fail()
        {
            // arrange
            string emailAdd = "example@gmail.com";
            Password password = new Password("Aa12345");

            //act
            userCtrl.Register(emailAdd, password.Password_);

            //assert
            Assert.IsTrue(userCtrl.Register(emailAdd, password.Password_).ErrorOccured, "Error didnt occured while register existing user");
        }

        [Test]
        public void Resgister_RegisterRegisteredUser_Fail()
        {
            // arrange
            string emailAdd = "ample@gmail.com";
            string pass = "Aa123456";

            //act
            userCtrl.Register(emailAdd, pass);

            //assert
            Assert.IsTrue(userCtrl.Register(emailAdd, pass).ErrorOccured, "shouldnt register registered user");
        }

        [Test]
        public void Resgister_RegisterValidInput_Success()
        {
            // arrange
            string emailAdd = "example@gmail.com";
            Password password = new Password("Aa12345");

            //act
            userCtrl.Register(emailAdd, password.Password_);

            //assert
            Assert.IsFalse(userCtrl.containsEmail(emailAdd).ErrorOccured, "couldnt register a legit user");
        }
    }
}