using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Servicios.api.Libreria.Repository.AutorRepository;

namespace Servicios.api.Libreria.Core.Entities
{
    [BsonIgnoreExtraElements]
    public class Autor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [DataMember]
        [BsonElement("nombre")]
        public string nombre { get; set; }

        [DataMember]
        [BsonElement("apellido")]
        public string apellido { get; set; }

        [DataMember]
        [BsonElement("gradoAcademico")]
        public string gradoAcademico { get; set; }

    }
}
