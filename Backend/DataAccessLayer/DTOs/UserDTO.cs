using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserDTO : DTO
    {
        public const string EmailColumnName = "email";
        public const string PasswordColumnName = "password";


        private string _email;
        public string Email { get => _email; set { if (_controller.Update(_email, EmailColumnName, EmailColumnName, value)) { _email = value; } } }

        private string _password;
        public string Password { get => _password; set { if (_controller.Update(_email, EmailColumnName, PasswordColumnName, value)) { _password = value; } } }


        public UserDTO(string email, string password) : base(new UserDALController())
        {
            _email = email;
            _password = password;
        }

        /// <summary>
        /// This function insert new User into the dataBase
        /// </summary>
        /// <returns> This function return True if there are any records effected in the dataBase </returns>
        public override bool Insert()
        {
            return _controller.Insert(this);
        }
        /// <summary>
        /// This function delete user from the dataBase
        /// </summary>
        /// <returns> This function return True if there are any records effected in the dataBase </returns>
        public override bool Delete()
        {
            return _controller.Delete(this);
        }


    }
}
