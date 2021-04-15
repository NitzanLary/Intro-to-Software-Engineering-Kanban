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
        //private static UserController instance;
        private readonly PasswordController pc;
        private Dictionary<string, User> users;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public UserController()
        {
            //pc = PasswordController.GetInstance();
            pc = new PasswordController();
            users = new Dictionary<string, User>();
        }

        //public static UserController GetInstance()
        //{
        //    if (instance == null)
        //        instance = new UserController();
        //    return instance;
        //}

        /// <summary>        
        /// checks if the given email address is valid
        /// </summary>
        /// <param name="emailaddress">The email of the user to register</param>
        /// <returns>A true if valid else false</returns>
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

        ///<summary>This method registers a new user to the system.</summary>
        ///<param name="email">the user e-mail address, used as the username for logging the system.</param>
        ///<param name="password">the user password.</param>
        ///<returns cref="Response">The response of the action</returns>
        public Response Register(string email, string password)
        {
            if (email == null || password == null)
                return new Response("Null is not optional");
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

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            if (email == null || password == null)
                return Response<User>.FromError("Null is not optional");
            if (!users.ContainsKey(email))
            {
                string s = "User not found";
                log.Warn(s);
                return Response<User>.FromError(s);
            }
            //log.Info($"User {email} Login successfully!");
            return users[email].Login(password);
        }

        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            if (email == null)
                return new Response("Null is not optional");
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
            if (email == null)
                return Response<User>.FromError("Null is not optional");
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
            if (email == null)
                return Response<bool>.FromError("Null is not optional");
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