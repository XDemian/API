using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Models.Usuarios
{
    public class LoginViewModelInput
    {
       [Required(ErrorMessage = "O Login é Obrigatorio")]
        public string Login  { get; set; }

        [Required(ErrorMessage = "A senha é Obrigatoria")]
        public string  Senha { get; set; }

    }
}
