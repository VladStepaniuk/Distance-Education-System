using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DESystem.Models
{
    public class UserModelDTO
    {
        public UserModelDTO(string FullName, string Email, string UserName, DateTime DateCreated, string Role)
        {
            this.FullName = FullName;
            this.Email = Email;
            this.UserName = UserName;
            this.DateCreated = DateCreated;
            this.Role = Role;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
