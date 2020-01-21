using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System.Linq;

namespace MongoDBDemo
{
    public class DemoService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            MongoDBTest();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void MongoDBTest()
        {
            string MongoDbConnectString = "mongodb+srv://115.159.223.241";

            var credential = MongoCredential.CreateCredential("admin", "AdminUser", "1qaz@WSX3edc");
            var setting = new MongoClientSettings()
            {
                Credential = credential,
                Server = new MongoServerAddress("115.159.223.241"),
            };
            // 连接
            var client = new MongoClient(setting);

            // 列出所有db
            using(var cursor = client.ListDatabaseNames())
            {
                var dbnames = cursor.ToList();
                dbnames.ForEach(n =>
                {
                    Console.WriteLine(n);
                });
            }

            // 新建/获取db(第一次使用就新建数据库)
            var database = client.GetDatabase("spider");

            // 获取collections
            var collectionNames = database.ListCollectionNames().ToList();
            if (collectionNames.Count >0 )
            {
                collectionNames.ForEach(p => { Console.WriteLine(p); });
                var collections = database.GetCollection<BsonDocument>(collectionNames.First());
            }

            if (!collectionNames.Contains("MyColl"))
            {

                // 新建Collection,带选项
                var option = new CreateCollectionOptions
                {
                    Capped = true,
                    MaxSize = 10000
                };

                database.CreateCollection("MyColl", option);
            }

            

            Nover nover = new Nover()
            {
                chapName = $"Chap{DateTime.Now.ToString("yyyyMMddHHmmssfff")}",
                Content = "test",
                Now = DateTime.Now
            };

            var collection = database.GetCollection<Nover>("MyColl");
            // 查询
            collection.InsertOne(nover);

            // 删除


        }

    }

    public class Nover
    {
        public string chapName { get; set; }
        public string Content { get; set; }
        public DateTime Now { get; set; }
    }

}
