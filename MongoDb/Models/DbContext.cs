using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDb.Models
{
    public class DbContext<T>
        where T : BaseModel
    {
        //定义数据库
        private readonly IMongoDatabase _database = null;
        public DbContext(IMongoDatabase database)
        {
            _database = database;
            //连接服务器名称  mongo的默认端口27017
            //var client = new MongoClient("mongodb://swen:swen123456@10.10.10.11:27017");
            //if (client != null)
            //    //连接数据库
            //    _database = client.GetDatabase("app");
        }

        private IMongoCollection<T> DbSet
        {
            get
            {
                var dbName = typeof(T).Name;
                return _database.GetCollection<T>(dbName);
            }
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public async Task<PageModel<T>> Get(int pageIndex = 1, int pageSize = 10, Expression<Func<T, object>> orderBy = null, Expression<Func<T, bool>> filter = null, bool isOrder = true)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            var data = isOrder ?
                 DbSet.Find(filter).SortBy(orderBy) :
                 DbSet.Find(filter).SortByDescending(orderBy);


            long total = await data.CountDocumentsAsync();
            return new PageModel<T>
            {
                dataCount = total,
                pageCount = (Math.Ceiling(total.ObjToDecimal() / pageSize.ObjToDecimal())).ObjToInt(),
                pageIndex = pageIndex,
                PageSize = pageSize,
                data = await data.Skip(pageSize * (pageIndex - 1)).Limit(pageSize).ToListAsync()
            };
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> Get(string id)
        {
            T info = await DbSet.Find<T>(T => T.Id == id).FirstOrDefaultAsync();
            return info;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public async Task<T> Create(T T)
        {
            T.AddTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);// mongodb里面存的格林时间;
            await DbSet.InsertOneAsync(T);
            return T;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TIn"></param>
        public async Task Update(string id, T TIn)
        {
            TIn.ModifyTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);// mongodb里面存的格林时间;
            await DbSet.ReplaceOneAsync(T => T.Id == id, TIn);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="TIn"></param>
        public async Task Remove(T TIn)
        {
            await DbSet.DeleteOneAsync(T => T.Id == TIn.Id);
        }

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        public async Task Remove(string id)
        {
            await DbSet.DeleteOneAsync(T => T.Id == id);
        }
    }
}
