using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO_s
{
    public class CreateUserDto
    {
        public int RoleId { get; set; }
        public string LastName { get; set;}
        public string Name { get; set;}
        public string Patronymic {  get; set;}
        public string Login { get; set;}
        public string PasswordHash { get; set;}
    }
}
