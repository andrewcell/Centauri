using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Centauri.Models
{
    public class MySQL
    {
        public struct Result
        {

            public bool Success;
            public List<string> Data;

            public Result(bool success)
            {
                Success = success;
                Data = new List<string>();
            }

        }
        private string ConnectionUri { get; set; }
        public MySQL(string id, string password, string host, short port, string database)
        {
            ConnectionUri = @"server=" + host + ";Port=" + port + ";Uid=" + id + ";password=" + password + ";database=" +
                            database;
            Console.WriteLine("MySQL Initialized");
        }

        public MySQL(string uri)
        {
            ConnectionUri = uri;
            Console.WriteLine("MySQL Initialized");
        }

        public Result ConnectionTest()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(ConnectionUri);
                connection.Open();
                Result rst = new Result
                {
                    Success = true,
                    
                };
                rst.Data.Add(connection.ServerVersion);
                connection.Close();
                return rst;

            }
            catch (Exception e)
            {
                Result rst = new Result(false);

                rst.Data.Add(e.Message);
                return rst;

            }
        }

        public static Result ConnectionTest(string id, string password, string host, short port, string database)
        {
            string uri = @"server=" + host + ":" + port + ";userid=" + id + ";password=" + password + ";database=" +
                            database;
            try
            {
                MySqlConnection connection = new MySqlConnection(uri);
                Result rst = new Result
                {
                    Success = true,
                    Data = {[0] = connection.ServerVersion},
                };
                return rst;

            }
            catch (MySqlException e)
            {
                Result rst = new Result
                {
                    Success = false,
                    Data = {[0] = e.ToString()}
                };
                return rst;
            }
        }
    }
}
