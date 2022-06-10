using curso.api.Business.Entities;
using System.Collections.Generic;

namespace curso.api.Business.Repositories
{
    public interface ICursoRepository
    {
        public void Adicionar(Curso Curso);

        public void Commit();
        IList<Curso> ObterPorUsuario(int codigoUsuario);
    }
}
