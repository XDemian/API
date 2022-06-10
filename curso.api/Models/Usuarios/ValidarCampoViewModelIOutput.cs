using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Models.Usuarios
{
    public class ValidarCampoViewModelIOutput
    {
        public IEnumerable<string> Erros { get; private set; }
        public ValidarCampoViewModelIOutput(IEnumerable<string>  erros)
        {
            Erros = erros;
        }
    }
}
