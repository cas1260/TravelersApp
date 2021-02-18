using System;
using System.Collections.Generic;
using System.Text;

namespace AppIsphereTravelers.Models
{
    public class UserLogin
    {
        public Boolean Sucesso { get; set; }
        public Boolean Primeira_vez { get; set; }
        public int Id { get; set; }
        public Boolean Cadastro { get; set; }
        public string name_profile { get; set; }

        public string email_profile { get; set; }
        public string birthday_profile { get; set; }
        public string photo_profile { get; set; }
        public string  IdiomaPadrao { get; set; }
    }
}
