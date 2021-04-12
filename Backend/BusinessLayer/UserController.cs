    using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class UserController
    {
        private static UserController instance;
        private readonly PasswordController pc;
        private Dictionary<string, User> users;

        private UserController()
        {
            pc = PasswordController.GetInstance();
            users = new Dictionary<string, User>();
        }

        public static UserController GetInstance()
        {
            if (instance == null)
                instance = new UserController();
            return instance;
        }


        public Response Register(string email, string password)
        {
            if (users.ContainsKey(email))
                return new Response("User already registered");
            Response<Password> rPass = pc.createPassword(password);
            if (rPass.ErrorOccured)
                return rPass;
            User user = new User(email, rPass.Value);
            users.Add(email, user);
            return new Response();
        }

        public Response<User> Login(string email, string password)
        {
            if (!users.ContainsKey(email))
                return Response<User>.FromError("User not found");
            return users[email].Login(password);
        }

        public Response Logout(string email)
        {
            if (!users.ContainsKey(email))
                return new Response("User not found");
            return users[email].logout();
        }

        public Response<User> getUserByEmail(string email)
        {
            if (!users.ContainsKey(email))
                return Response<User>.FromError("User not found");
            return Response<User>.FromValue(users[email]);
        }

        public Response<bool> isLoggedIn(string email)
        {
            if (!users.ContainsKey(email))
                return Response<bool>.FromError("User not found");
            return Response<bool>.FromValue(users[email].IsLoggedIn);
        }

    }
}