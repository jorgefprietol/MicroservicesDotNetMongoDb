using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Repository
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> GetAutores();
        Autor InsertAutor(Autor autor);
        Autor DelAutor(Autor id);
        Autor PutAutor(Autor autor);
    }
}
