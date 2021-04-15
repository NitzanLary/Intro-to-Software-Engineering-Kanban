    using IntroSE.Kanban.Backend.ServiceLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class UserController
    {
        private static UserController instance;
        private readonly PasswordController pc;
        private Dictionary<string, User> users;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


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

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public Response Register(string email, string password)
        {
            if (users.ContainsKey(email))
            {
                string s = $"User {email} already registered";
                log.Warn(s);
                return new Response(s);
            }
            if (!IsValidEmail(email))
            {
                string s = $"{email} is not real valid email";
                log.Warn(s);
                return new Response(s);
            }
                 
            Response<Password> rPass = pc.createPassword(password);
            if (rPass.ErrorOccured)
                return rPass;
            User user = new User(email, rPass.Value);
            users.Add(email, user);
            log.Info($"{email} successfully Registered!");
            return new Response();
        }

        public Response<User> Login(string email, string password)
        {
            if (!users.ContainsKey(email))
            {
                string s = "User not found";
                log.Warn(s);
                return Response<User>.FromError(s);
            }
            //log.Info($"User {email} Login successfully!");
            return users[email].Login(password);
        }

        public Response Logout(string email)
        {
            if (!users.ContainsKey(email))
            {
                string s = $"User {email} not found";
                log.Warn(s);
                return new Response("User not found");
            }
            //log.Info($"User {email} Logout successfully!");
            return users[email].logout();
        }

        public Response<User> getUserByEmail(string email)
        {
            if (!users.ContainsKey(email))
            {
                string s = $"User {email} not found";
                log.Warn(s);
                return Response<User>.FromError(s);
            }
            return Response<User>.FromValue(users[email]);
        }

        public Response<bool> isLoggedIn(string email)
        {
            if (!users.ContainsKey(email))
            {
                string s = $"User {email} not found";
                log.Warn(s);
                return Response<bool>.FromError(s);
            }
            return Response<bool>.FromValue(users[email].IsLoggedIn);
        }

    }
}