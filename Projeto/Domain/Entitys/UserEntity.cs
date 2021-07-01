using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
   public class UserEntity : BaseEntity
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int IdLogradouro { get; set; }

    }
}
