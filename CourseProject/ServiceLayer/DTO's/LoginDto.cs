using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO_s
{
    public class LoginDto
    {
        public string Login {  get; set; }
        public string PasswordHash { get; set; }
    }
}
