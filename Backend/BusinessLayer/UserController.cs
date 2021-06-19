using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.BusinessLayer.objects;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    /// <summary>
    /// This class is a singleton
    /// </summary>
    internal class UserController
    {
        private readonly PasswordController pc;
        private Dictionary<string, IUser> users;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static UserController instance;

        private UserController()
        {
            pc = new PasswordController();
            users = new Dictionary<string, IUser>();
        }

        public static UserController GetInstance()
        {
            if (instance == null)
                instance = new UserController();
            return instance;
        }

        public MFResponse<List<User>> LoadDate()
        {
            List<User> curr_users = new List<User>();
            try
            {
                List<UserDTO> userDTOs = new UserDALController().SelectAllUsers();
                foreach(UserDTO dto in userDTOs)
                {
                    User user = new(dto);
                    users.Add(dto.Email, user);
                    curr_users.Add(user);
                }
            }
            catch(Exception e)
            {
                return MFResponse<List<User>>.FromError(e.Message);
            }
            return MFResponse<List<User>>.FromValue(curr_users);
        }

        public MFResponse DeleteData()
        {
           
            new UserDALController().DeleteAllData();
            users = new Dictionary<string, IUser>();

            return new MFResponse();
        }

        /// <summary>        
        /// checks if the given email address is valid
        /// </summary>
        /// <param name="emailaddress">The email of the user to register</param>
        /// <returns>A true if valid else false</returns>
        public bool IsValidEmail(string emailaddress)
        {
            return Regex.IsMatch(emailaddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        internal MFResponse<Password> createPassword(string password)
        {
            return pc.createPassword(password);
        }

        public bool ContainsUser(string email)
        {
            return users.ContainsKey(email);
        }
        ///<summary>This method registers a new user to the system.</summary>
        ///<param name="email">the user e-mail address, used as the username for logging the system.</param>
        ///<param name="password">the user password.</param>
        ///<returns cref="MFResponse">The response of the action</returns>
        public MFResponse Register(string email, string password)
        {
            if (email == null || password == null)
                return new MFResponse("Null is not optional");
            if (ContainsUser(email))
            {
                string s = $"User {email} already registered";
                log.Warn(s);
                return new MFResponse(s);
            }
            if (!IsValidEmail(email))
            {
                string s = $"{email} is not real valid email";
                log.Warn(s);
                return new MFResponse(s);
            }
                 
            MFResponse<Password> rPass = createPassword(password);
            if (rPass.ErrorOccured)
                return rPass;

            User user = new User(email, rPass.Value);
            users.Add(email, user);
            log.Info($"{email} successfully Registered!");
            return new MFResponse();
        }
        /// <summary>
        /// overloading register for tests useage
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public MFResponse<IUser> InsertExistingUser(IUser u)
        {
            if (!users.ContainsKey(u.Email))
                users.Add(u.Email, u);
            return MFResponse<IUser>.FromValue(users[u.Email]);
        }

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public MFResponse<IUser> Login(string email, string password)
        {
            if (email == null || password == null)
                return MFResponse<IUser>.FromError("Null is not optional");
            if (!ContainsUser(email))
            {
                string s = "User not found";
                log.Warn(s);
                return MFResponse<IUser>.FromError(s);
            }
            //log.Info($"User {email} Login successfully!");
            IUser u = users[email];
            return users[email].Login(password);
        }

        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse Logout(string email)
        {
            if (email == null)
                return new MFResponse("Null is not optional");
            if (!ContainsUser(email))
            {
                string s = $"User {email} not found";
                log.Warn(s);
                return new MFResponse("User not found");
            }
            //log.Info($"User {email} Logout successfully!");
            return users[email].logout();
        }

        /// <summary>        
        /// check if users contains the given email. 
        /// </summary>
        /// <param name="email">The email of the user to cehck if contains</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        internal MFResponse containsEmail(string email)
        {
            if (email == null)
                return new MFResponse("Null is not optional");
            if (!ContainsUser(email))
            {
                string s = $"User {email} not found";
                log.Warn(s);
                return new MFResponse(s);
            }
            return new MFResponse();
        }

        /// <summary>        
        /// Returns user according to the given email. 
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse<User> getUserByEmail(string email)
        {
            if (email == null)
                return MFResponse<User>.FromError("Null is not optional");
            if (!ContainsUser(email))
            {
                string s = $"User {email} not found";
                log.Warn(s);
                return MFResponse<User>.FromError(s);
            }
            return MFResponse<User>.FromValue((User)users[email]);
        }

        /// <summary>        
        /// check if the email is logged in. 
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse<bool> isLoggedIn(string email)
        {
            if (email == null)
                return MFResponse<bool>.FromError("Null is not optional");
            if (!ContainsUser(email))
            {
                string s = $"User {email} not found";
                log.Warn(s);
                return MFResponse<bool>.FromError(s);
            }
            return MFResponse<bool>.FromValue(users[email].IsLoggedIn);
        }
    }
}