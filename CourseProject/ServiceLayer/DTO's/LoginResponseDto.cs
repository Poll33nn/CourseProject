using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO_s
{
    public class LoginResponseDto
    {
        public string UserFullName { get; set; }
        public string UserRole { get; set; }
        public string Token { get; set; }
    }
}
