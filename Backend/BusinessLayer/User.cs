using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class User
    {
        
        private string email;
        public string Email
        {
            get => email;
        }
        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get => isLoggedIn;
            set => isLoggedIn = value;
        }
        private Password password;
        private UserDTO dto;
        public UserDTO DTO
        {
            get => dto;
            private set => dto = value;
        }

        public User(string email, Password password)
        {
            this.email = email;
            this.password = password;
            dto = new UserDTO(email, password.Password_);
            dto.Insert();
        }

        public User(UserDTO userDTO)
        {
            email = userDTO.Email;
            password = new Password(userDTO.Password);
            dto = userDTO;
        }

        /// <summary>        
        /// Login an unlogged user. 
        /// </summary>
        /// <param name="password">The password of the user to log in</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response<User> Login(string password)
        {
            if (!IsPasswordCorrect(password))
                return Response<User>.FromError("Incorrect Password");
            if (isLoggedIn)
                return Response<User>.FromError("User is already loogged in");
            IsLoggedIn = true;
            return Response<User>.FromValue(this);
        }
        
        public Response addBoard(string boardName)
        {
            throw new NotImplementedException();
        }

        public Response removeBoard(string boardName)
        {
            throw new NotImplementedException();
        }

        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response logout()
        {
            if (!isLoggedIn)
                return Response<User>.FromError("User is not loogged in");
            IsLoggedIn = false;
            return new Response();
        }

        /// <summary>        
        /// Validattion of the given passowrd.
        /// </summary>
        /// <param name="password">The password of the user to log in</param>
        /// <returns> true if the passowrd correct else false. </returns>
        private bool IsPasswordCorrect(string password)
        {
            return this.password.IsMatch(password);
        }
    }
}
