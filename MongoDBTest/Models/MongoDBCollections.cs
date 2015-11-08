using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson; //必載
using MongoDB.Driver; //必載

namespace MongoDBTest.Models
{
    public class MongoDBCollections
    {
        public static IMongoCollection<BsonDocument> GetCollection_ProxyAPICall()
        {
            IMongoCollection<BsonDocument> collection = null;
            try
            {
                var client = new MongoClient("mongodb://H3HybridMain:2wsx$RFV6yhn@210.64.215.244:27017/Log");
                //MongoDB.Driver 2.0.1
                var database = client.GetDatabase("Log");

                //MongoDB.Driver 1.10
                //var server = client.GetServer();
                //var database = server.GetDatabase("Log");

                collection = database.GetCollection<BsonDocument>("ProxyAPICall");
            }
            catch (Exception ex)
            {
                object result = ex.ToString();
            }
            return collection;
        }
    }
}