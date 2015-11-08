using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks; //必載 for Task
using MongoDB.Bson; //必載 for BsonDocument
using MongoDB.Bson.Serialization; // 必載 for IMongoCollection
using MongoDB.Bson.Serialization.Attributes; //必載 for BsonId
using MongoDB.Bson.Serialization.IdGenerators; //必載 for StringObjectIdGenerator
using MongoDB.Driver;
//using MongoDB.Driver.Builders; //必載 for MongoDB.Driver 1.10


namespace MongoDBTest.Models
{
    /// <summary>
    /// ProxyAPI呼叫記錄
    /// </summary>
    public class ProxyAPICallModels
    {
        #region public 屬性
        //http://codingcanvas.com/using-mongodb-_id-field-with-c-pocos/
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string ID { get; set; }
        public string API_Name { get; set; }
        public string Parameters { get; set; }
        public string API_CName { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnData { get; set; }
        public string Description { get; set; }
        public string ExecuteUser { get; set; }
        //http://dotnetcodr.com/2014/08/04/mongodb-in-net-part-3-starting-with-poco-documents/
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CTime { get; set; }
        #endregion

        #region 建構子
        /// <summary>
        /// 建構子
        /// </summary>
        public ProxyAPICallModels()
        {

        }

        /// <summary>
        /// 建構子
        /// </summary>
        public ProxyAPICallModels(BsonDocument doc)
        {
            this.ID = doc.GetValue("_id", string.Empty).ToString().Replace("ObjectId(", "").Replace(")", "");
            this.API_Name = doc.GetValue("API_Name", string.Empty).ToString();
            this.Parameters = doc.GetValue("Parameters", string.Empty).ToString();
            this.API_CName = doc.GetValue("API_CName", string.Empty).ToString();
            this.ReturnCode = doc.GetValue("ReturnCode", string.Empty).ToString();
            this.ReturnData = doc.GetValue("ReturnData", string.Empty).ToString();
            this.Description = doc.GetValue("Description", string.Empty).ToString();
            this.ExecuteUser = doc.GetValue("ExecuteUser", string.Empty).ToString();
            DateTime tmp;
            DateTime.TryParse(doc.GetValue("CTime", DateTime.UtcNow).ToString(), out tmp);
            this.CTime = tmp.ToLocalTime();
        }
        #endregion

        #region 方法
        /// <summary>
        /// ProxyAPI呼叫記錄 - 新增一筆
        /// </summary>
        /// <returns>"0":成功, 其它:失敗</returns>
        public object Creat()
        {
            object result = "0";
            try
            {
                var document = new BsonDocument
                {
                    {"API_Name", this.API_Name},
                    {"Parameters", BsonSerializer.Deserialize<BsonDocument>(this.Parameters) },
                    {"API_CName", this.API_CName},
                    {"ReturnCode", this.ReturnCode},
                    {"ReturnData", BsonSerializer.Deserialize<BsonDocument>(this.ReturnData) },
                    {"Description", this.Description},
                    {"ExecuteUser", this.ExecuteUser},
                    {"CTime", DateTime.UtcNow}
                };

                //MongoDB.Driver 2.0.1
                IMongoCollection<BsonDocument> collection = MongoDBCollections.GetCollection_ProxyAPICall(); //取得Log資料庫ProxyAPICall資料表
                Task.Run(async () =>
                {
                    await collection.InsertOneAsync(document);
                }).GetAwaiter().GetResult();

                //MongoDB.Driver 1.10
                //MongoCollection<BsonDocument> collection = MongoDBCollections.GetCollection_ProxyAPICall(); //取得Log資料庫ProxyAPICall資料表
                //collection.Insert(document);
                //var id = document.Id;
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        /// <summary>
        /// ProxyAPI呼叫記錄 - 取得一筆
        /// </summary>
        /// <returns>"0":成功, 其它:失敗</returns>
        public object GetOne(string ID)
        {
            object result = "0";
            try
            {
                //MongoDB.Driver 2.0.1
                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(ID));
                ProxyAPICallModels pac = new ProxyAPICallModels();

                IMongoCollection<BsonDocument> collection = MongoDBCollections.GetCollection_ProxyAPICall(); //取得Log資料庫ProxyAPICall資料表
                Task.Run(async () =>
                {
                    var doc = await collection.Find(filter).FirstAsync();
                    this.ID = doc.GetValue("_id", string.Empty).ToString().Replace("ObjectId(", "").Replace(")", "");
                    this.API_Name = doc.GetValue("API_Name", string.Empty).ToString();
                    this.Parameters = doc.GetValue("Parameters", string.Empty).ToString();
                    this.API_CName = doc.GetValue("API_CName", string.Empty).ToString();
                    this.ReturnCode = doc.GetValue("ReturnCode", string.Empty).ToString();
                    this.ReturnData = doc.GetValue("ReturnData", string.Empty).ToString();
                    this.Description = doc.GetValue("Description", string.Empty).ToString();
                    this.ExecuteUser = doc.GetValue("ExecuteUser", string.Empty).ToString();
                    DateTime tmp;
                    DateTime.TryParse(doc.GetValue("CTime", DateTime.UtcNow).ToString(), out tmp);
                    this.CTime = tmp.ToLocalTime();
                }).GetAwaiter().GetResult();

                //MongoDB.Driver 1.10
                //MongoCollection<BsonDocument> collection = MongoDBCollections.GetCollection_ProxyAPICall(); //取得Log資料庫ProxyAPICall資料表
                //var query = Query.EQ("_id", ObjectId.Parse(ID));
                //var doc = collection.FindOne(query);

                //this.ID = doc.GetValue("_id", string.Empty).ToString().Replace("ObjectId(", "").Replace(")", "");
                //this.API_Name = doc.GetValue("API_Name", string.Empty).ToString();
                //this.Parameters = doc.GetValue("Parameters", string.Empty).ToString();
                //this.API_CName = doc.GetValue("API_CName", string.Empty).ToString();
                //this.ReturnCode = doc.GetValue("ReturnCode", string.Empty).ToString();
                //this.ReturnData = doc.GetValue("ReturnData", string.Empty).ToString();
                //this.Description = doc.GetValue("Description", string.Empty).ToString();
                //this.ExecuteUser = doc.GetValue("ExecuteUser", string.Empty).ToString();
                //DateTime tmp;
                //DateTime.TryParse(doc.GetValue("CTime", DateTime.UtcNow).ToString(), out tmp);
                //this.CTime = tmp.ToLocalTime();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }
        #endregion

        #region 靜態方法
        /// <summary>
        /// ProxyAPI呼叫記錄 - 依條件取得
        /// </summary>
        /// <returns>"0":成功, 其它:失敗</returns>
        public static object GetList(string API_Name, string ReturnCode, string UserId, out List<ProxyAPICallModels> pacs)
        {
            pacs = new List<ProxyAPICallModels>();
            object result = "0";
            try
            {
                //MongoDB.Driver 2.0.1
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Ne("_id", ""); //預設ID不為空 => 全列

                if (!string.IsNullOrEmpty(API_Name))
                    filter = filter & builder.Regex("API_Name", ".*" + API_Name + ".*"); //字串部分比對
                if (!string.IsNullOrEmpty(ReturnCode))
                    filter = filter & builder.Eq("ReturnCode", ReturnCode);
                if (!string.IsNullOrEmpty(UserId))
                    filter = filter & builder.Regex("Parameters.UserId", "/" + UserId + "/"); //字串部分比對

                var collection = MongoDBCollections.GetCollection_ProxyAPICall(); //取得Log資料庫ProxyAPICall資料表
                List<BsonDocument> docs = new List<BsonDocument>();
                Task.Run(async () =>
                {
                    var tmp = await collection.Find(filter).ToListAsync();
                    docs = tmp.ToList();
                }).GetAwaiter().GetResult();

                List<ProxyAPICallModels> models = new List<ProxyAPICallModels>();
                docs.ForEach(doc =>
                {
                    ProxyAPICallModels model = new ProxyAPICallModels(doc);
                    models.Add(model);
                }
                );
                pacs = models;

                //MongoDB.Driver 1.10
                //var query = Query.NE("_id", ""); //預設ID不為空 => 全列

                //if (!string.IsNullOrEmpty(API_Name))
                //    query = Query.And(query, Query.Matches("API_Name", API_Name)); //字串部分比對
                //if (!string.IsNullOrEmpty(ReturnCode))
                //    query = Query.And(query, Query.EQ("ReturnCode", ReturnCode));
                //if (!string.IsNullOrEmpty(UserId))
                //    query = Query.And(query, Query.Matches("Parameters.UserId", UserId)); //字串部分比對
                //if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
                //{
                //    query = Query.And(query, Query.GTE("CTime", DateTime.Parse(StartTime).ToLocalTime()));
                //    query = Query.And(query, Query.LTE("CTime", DateTime.Parse(EndTime).ToLocalTime()));
                //}

                //MongoCollection<BsonDocument> collection = MongoDBCollections.GetCollection_ProxyAPICall(); //取得Log資料庫ProxyAPICall資料表
                //var docs = collection.Find(query);

                //List<ProxyAPICallModels> models = new List<ProxyAPICallModels>();
                //foreach (var doc in docs)
                //{
                //    ProxyAPICallModels model = new ProxyAPICallModels(doc);
                //    models.Add(model);
                //}
                //pacs = models;
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }

            return result;
        }

        /// <summary>
        /// ProxyAPI呼叫記錄 - 取得全部
        /// </summary>
        /// <returns>"0":成功, 其它:失敗</returns>
        public static object GetAll(out List<ProxyAPICallModels> pacs)
        {            
            pacs = new List<ProxyAPICallModels>();
            object result = "0";
            try
            {
                //MongoDB.Driver 2.0.1
                IMongoCollection<BsonDocument> collection = MongoDBCollections.GetCollection_ProxyAPICall(); //取得Log資料庫ProxyAPICall資料表
                List<BsonDocument> docs = new List<BsonDocument>();
                Task.Run(async () =>
                {
                    var tmp = await collection.Find(new BsonDocument()).ToListAsync();
                    docs = tmp.ToList();
                }).GetAwaiter().GetResult();

                List<ProxyAPICallModels> models = new List<ProxyAPICallModels>();
                docs.ForEach(doc =>
                {
                    ProxyAPICallModels model = new ProxyAPICallModels(doc);
                    models.Add(model);
                }
                );
                pacs = models;

                //MongoDB.Driver 1.10
                //MongoCollection<BsonDocument> collection = MongoDBCollections.GetCollection_ProxyAPICall(); //取得Log資料庫ProxyAPICall資料表
                //var docs = collection.FindAll();

                //List<ProxyAPICallModels> models = new List<ProxyAPICallModels>();
                //foreach (var doc in docs)
                //{
                //    ProxyAPICallModels model = new ProxyAPICallModels(doc);
                //    models.Add(model);
                //}
                //pacs = models;
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }

            return result;
        }
        #endregion
        
    }
}