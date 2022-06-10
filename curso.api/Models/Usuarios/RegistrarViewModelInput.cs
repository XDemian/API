using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Models.Usuarios
{
    public class RegistrarViewModelInput
    {

        [Required(ErrorMessage = "O Login é Obrigatorio")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A Senha é Obrigatoria")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Email é obrigatorio")]
        public string Email { get; set; }

      
    }
}
