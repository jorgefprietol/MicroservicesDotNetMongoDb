using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Servicios.api.Libreria.Core;
using Servicios.api.Libreria.Core.ContextMongoDB;
using Servicios.api.Libreria.Core.Entities;
using Servicios.api.Libreria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaServicioController : ControllerBase
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IMongoRepository<AutorEntity> _mongoRepository;
        private readonly IMongoRepository<PepeEntity> _pepeRepository;
        private readonly IOptions<MongoSettings> _auc;

        public LibreriaServicioController(IAutorRepository autorRepository, IOptions<MongoSettings> options, IMongoRepository<AutorEntity> mongoRepository, IMongoRepository<PepeEntity> pepeRepository)
        {
            _autorRepository = autorRepository;
            _mongoRepository = mongoRepository;
            _pepeRepository = pepeRepository;
            _auc = options;
        }

        [HttpGet("pepeGenerico")]
        public async Task<ActionResult<IEnumerable<PepeEntity>>> GetPepeGenerico()
        {
            var autores = await _pepeRepository.GetAll();
            return Ok(autores);
        }

        [HttpGet("autorGenerico")]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> GetAutorGenerico()
        {
            var autores = await _mongoRepository.GetAll();
            return Ok(autores);
        }

        [HttpGet("autores")]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
        {
            var autores = await _autorRepository.GetAutores();
            return Ok(autores);
        }
        [HttpPost("insert-autor")]
        public Autor InsertAutor(Autor autor)
        {
            return _autorRepository.InsertAutor(autor);
        }

        [HttpDelete("del-autor")]
        public Autor DelAutor(Autor id)
        {
            return _autorRepository.DelAutor(id);
        }
        [HttpPut("put-autor")]
        public Autor PutAutor(Autor autor)
        {
            return _autorRepository.PutAutor(autor);
        }
        [HttpGet("lista")]
        public ActionResult<string[]> GetListaColecciones()
        {
            
            var client = new MongoClient(_auc.Value.ConnectionString);
            var _db = client.GetDatabase(_auc.Value.Database);
            var lista = _db.Client.ListDatabaseNames().ToList();
            return Ok(lista);
        }

        [HttpGet("lista-autores")]
        [Obsolete]
        public ActionResult<List<Autor>> GetListaAutores()
        {

            var client = new MongoClient(_auc.Value.ConnectionString);
            var _db = client.GetDatabase(_auc.Value.Database);
            Console.WriteLine("EJEMPLO -> " + _db.ToString());
            List<Autor> v = _db.GetCollection<Autor>("Autor").Find(Autor => true).ToList();
            return Ok(v);
        }

    }
}
