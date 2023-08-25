using MongoDB.Bson;
using MongoDB.Driver;
using Servicios.api.Libreria.Core.ContextMongoDB;
using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly IAutorContext _autorContext;
        public AutorRepository(IAutorContext autorContext)
        {
            _autorContext = autorContext;
        }
        public async Task<IEnumerable<Autor>> GetAutores()
        {
            return await _autorContext.Autores.Find(_ => true).ToListAsync();
        }

        public Autor InsertAutor(Autor autor)
        {
            //Autor autor = new Autor() { nombre = "Pepe", apellido = "aaaa", gradoAcademico = "Superior" };
            _autorContext.Autores.InsertOne(autor);
            return autor;
        }

        public Autor DelAutor(Autor autor)
        {
            //Autor autor = new Autor() { nombre = "Pepe", apellido = "aaaa", gradoAcademico = "Superior" };
            _autorContext.Autores.DeleteOne(a => a.Id == autor.Id);
            return autor;
        }

        public Autor PutAutor(Autor autor)
        {
            //Autor autor = new Autor() { nombre = "Pepe", apellido = "aaaa", gradoAcademico = "Superior" };
            _autorContext.Autores.ReplaceOne(a => a.Id == autor.Id, autor);
            return autor;
        }

    }
}
