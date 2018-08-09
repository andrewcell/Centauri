using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Centauri.Models
{
    public class MongoDB
    {
        private string uri;
        public MongoDB(string ConnectionUri)
        {
            uri = ConnectionUri;
            Console.WriteLine("MongoDB Initialized.");
        }

        public string TestConnection()
        {
            try
            {
                MongoClient mongo = new MongoClient(uri);
                IMongoDatabase db = mongo.GetDatabase("Centauri");
                db.RunCommandAsync((Command<BsonDocument>) "{ping:1}").Wait();
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
