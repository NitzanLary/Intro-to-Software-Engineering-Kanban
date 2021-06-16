using NUnit.Framework;
using IntroSE.Kanban.Backend;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.ServiceLayer.Objects;
using Moq;
using IntroSE.Kanban.Backend.BusinessLayer;

namespace KanbanTests
{
    public class Tests
    {
        Service service;
        Mock<UserController> userCtrl;
        Mock<IntroSE.Kanban.Backend.BusinessLayer.User> user;

        [SetUp]
        public void Setup()
        {
            service = new Service();
            userCtrl = new Mock<UserController>();
        }

        [TearDown]
        public void TearDown()
        {
            service.DeleteData();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        public void AddTask_AddingNewTask_Success()
        {
            //
            TearDown();
            string u1 = "user1@gmail.com";
            string p1 = "Aa1234";
            string b1 = "board1";
            string c1 = "column1";
            string t1 = "task1";
            service.Register(u1,p1);
            service.AddBoard(u1, b1);
            service.AddColumn(u1,u1,b1,2, c1);
            service.AddTask(u1, u1, b1, t1, t1 + " desc", System.DateTime.Parse("05/20/2024"));
            //
            service = new Service();
            service.LoadData();
            //
            Assert.AreEqual(b1, service.GetBoard(u1, u1, b1).Value.name);
            Assert.AreEqual(c1, service.GetColumn(u1,u1,b1,0).Value.)
        }
        public void AddTask_AddingNewTaskToFullColumn_Fail()
        {

        }
        public void RemoveTaskTest_RemoveExistingTask_Success()
        {

        }
        public void RemoveTaskTest_RemoveInexistingTask_Fail()
        {

        }
        public void UpdateTaskTest_UpdateTask()
        {

        }
        public void Login_LoginValidUser_Success()
        {
            
        }
        public void Login_LoginInvalidUser_Fail()
        {

        }
        public void Logout_LogoutLoggedInUser_Success()
        {
            //arrange
            string emailAdd = "example@gmail.com";
            Password password = new Password("Aa12345");
            userCtrl.Setup(m => m.IsValidEmail(emailAdd)).Returns(true);
            userCtrl.Setup(m => m.createPassword(password.Password_)).Returns(MFResponse<Password>.FromValue(password));
            userCtrl.Object.Register(emailAdd, password.Password_);
            //act
            userCtrl.Object.Login(emailAdd, password.Password_);
            //assert
            Assert.IsTrue(userCtrl.Object.Login(emailAdd, password.Password_).ErrorOccured);
        }
        public void Logout_LogoutLoggedOutUser_Fail()
        {
            //arrange
            string emailAdd = "example@gmail.com";
            Password password = new Password("Aa12345");
            userCtrl.Setup(m => m.IsValidEmail(emailAdd)).Returns(true);
            userCtrl.Setup(m => m.createPassword(password.Password_)).Returns(MFResponse<Password>.FromValue(password));
            userCtrl.Object.Register(emailAdd, password.Password_);
            //act
            userCtrl.Object.Login(emailAdd, password.Password_);
            userCtrl.Object.Logout(emailAdd);
            //assert
            Assert.IsTrue(userCtrl.Object.Login(emailAdd, password.Password_).ErrorOccured);

        }
        [Test]
        public void Resgister_RegisterExistingUser_Fail()
        {
            // arrange
            string emailAdd = "example@gmail.com";
            Password password = new Password("Aa12345");
            userCtrl.Setup(m => m.IsValidEmail(emailAdd)).Returns(true);
            userCtrl.Setup(m => m.createPassword(password.Password_)).Returns(MFResponse<Password>.FromValue(password));

            //act
            userCtrl.Object.Register(emailAdd, password.Password_);

            //assert
            Assert.IsTrue(userCtrl.Object.Register(emailAdd, password.Password_).ErrorOccured);
        }

        [Test]
        public void Resgister_RegisterIvalidInput_Fail()
        {
            // arrange
            string emailAdd = "example@gmail.com";
            Password password = new Password("Aa12345");
            userCtrl.Setup(m => m.IsValidEmail(emailAdd)).Returns(true);
            userCtrl.Setup(m => m.createPassword(password.Password_)).Returns(MFResponse<Password>.FromValue(password));

            //act
            userCtrl.Object.Register(emailAdd, password.Password_);

            //assert
            Assert.IsTrue(userCtrl.Object.Register(emailAdd, password.Password_).ErrorOccured);
        }

        [Test]
        public void Resgister_RegisterValidInput_Success()
        {
            // arrange
            string emailAdd = "example@gmail.com";
            Password password = new Password("Aa12345");
            userCtrl.Setup(m => m.IsValidEmail(emailAdd)).Returns(true);
            userCtrl.Setup(m => m.createPassword(password.Password_)).Returns(MFResponse<Password>.FromValue(password));

            //act
            userCtrl.Object.Register(emailAdd, password.Password_);

            //assert
            Assert.IsFalse(userCtrl.Object.containsEmail(emailAdd).ErrorOccured);
        }
    }
}