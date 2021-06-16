using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    interface IUserController
    {
        /// <summary>        
        /// checks if the given email address is valid
        /// </summary>
        /// <param name="emailaddress">The email of the user to register</param>
        /// <returns>A true if valid else false</returns>
        public bool IsValidEmail(string emailaddress)
        {
            return Regex.IsMatch(emailaddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
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
            if (!users.ContainsKey(email))
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
            if (!users.ContainsKey(email))
            {
                string s = $"User {email} not found";
                log.Warn(s);
                return MFResponse<User>.FromError(s);
            }
            return MFResponse<User>.FromValue(users[email]);
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
            if (!users.ContainsKey(email))
            {
                string s = $"User {email} not found";
                log.Warn(s);
                return MFResponse<bool>.FromError(s);
            }
            return MFResponse<bool>.FromValue(users[email].IsLoggedIn);
        }
    }
}
