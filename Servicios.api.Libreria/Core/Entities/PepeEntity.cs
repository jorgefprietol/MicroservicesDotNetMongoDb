using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.Entities
{
    [BsonCollection("pepe")]
    public class PepeEntity : Document
    { 
        [BsonElement("nombre")]
        public string Nombre { get; set; }
    }
}
