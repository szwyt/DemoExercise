using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDb.Models
{
    public class Province : BaseModel
    {
        [BsonElement(nameof(Name))]
        public string Name { get; set; }

        [BsonElement(nameof(Age))]
        public int Age { get; set; }
    }
}
