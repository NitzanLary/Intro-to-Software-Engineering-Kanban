using IntroSE.Kanban.Backend.BusinessLayer.objects;
using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class User : IUser
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
        public virtual MFResponse<IUser> Login(string password)
        {
            if (!IsPasswordCorrect(password))
                return MFResponse<IUser>.FromError("Incorrect Password");
            if (isLoggedIn)
                return MFResponse<IUser>.FromError("User is already loogged in");
            IsLoggedIn = true;
            return MFResponse<IUser>.FromValue(this);
        }
        
        public MFResponse addBoard(string boardName)
        {
            throw new NotImplementedException();
        }

        public MFResponse removeBoard(string boardName)
        {
            throw new NotImplementedException();
        }

        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public MFResponse logout()
        {
            if (!isLoggedIn)
                return MFResponse<User>.FromError("User is not logged in");
            IsLoggedIn = false;
            return new MFResponse();
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
