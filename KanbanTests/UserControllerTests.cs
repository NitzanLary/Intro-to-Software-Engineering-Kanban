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
            System.Console.WriteLine(user.Object.GetHashCode() + " created");
            user.Setup(m => m.Email).Returns(email);
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
            int gotThere = 0;
            user.Setup(m=>m.Login(password)).Returns(MFResponse<IUser>.FromValue(user.Object)).Callback(()=>gotThere++);
            userCtrl.InsertExistingUser(user.Object);

            //act
            MFResponse<IUser> res = userCtrl.Login(email, password);

            //assert
            Assert.IsFalse(res.ErrorOccured, "error occured while login legitimate user");
            Assert.AreEqual(1, gotThere, "user's Login never called");
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

        [TestCase("unreg@gamil.com")]
        [TestCase(null)]
        public void IsLoggedIn_InvalidArguments_Fail(string badEmail)
        {
            //arrange
            string uregEmail = badEmail;
            Mock<IUser> unreg = new Mock<IUser>();
            unreg.Setup(m => m.Email).Returns(uregEmail);
            unreg.Setup(m => m.IsLoggedIn).Returns(true);

            //act
            MFResponse<bool> res = userCtrl.isLoggedIn(uregEmail);

            //assert
            Assert.IsTrue(res.ErrorOccured, "error didnot occured while looged in invalid user");
        }

        [Test]
        public void IsLoggedIn_RegisteredUser_Success()
        {
            //arrange
            int gotThere = 0;
            user.Setup(m => m.IsLoggedIn).Returns(true).Callback(()=>gotThere++);
            userCtrl.InsertExistingUser(user.Object);

            //act
            MFResponse<bool> res = userCtrl.isLoggedIn(email);

            //assert
            Assert.IsFalse(res.ErrorOccured, "Error occurred while check for login of logged-in user");
            Assert.AreEqual(1, gotThere, "user's IsLoggedInMethod never called");
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
            Assert.IsTrue(userCtrl.Register(emailAdd, pass).ErrorOccured, "Error didnt occured while register existing user");
        }

        [TestCase("ample@gmail.com", "1234")]
        [TestCase("ample@gmail", "Aa123456")]
        [TestCase(null, "1234")]
        public void Resgister_RegisterInvalidInput_Fail(string emailAdd, string pass)
        {
            // arrange
            //act
            MFResponse res = userCtrl.Register(emailAdd, pass);

            //assert
            Assert.IsTrue(res.ErrorOccured, "shouldnt register given user");
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