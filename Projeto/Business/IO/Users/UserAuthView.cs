using System;
using System.Collections.Generic;
using System.Text;

namespace Business.IO.Users
{
   public class UserAuthView
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
    }
}
