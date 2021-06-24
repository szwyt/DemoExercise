﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDb.Models
{
    public class BaseModel
    {
        [BsonId]        //标记主键
        [BsonRepresentation(BsonType.ObjectId)]     //参数类型  ， 无需赋值
        public string Id { get; set; }

        [BsonElement(nameof(AddTime))]   //指明数据库中字段名为CreateDateTime
        public DateTime AddTime
        {
            get;
            set;
        }

        [BsonElement(nameof(ModifyTime))]   //指明数据库中字段名为CreateDateTime
        public DateTime? ModifyTime
        {
            get;
            set;
        }

        [BsonElement(nameof(IsDelete))]
        public bool IsDelete { get; set; }

        public BaseModel()
        {
            IsDelete = false;
        }
    }
}
