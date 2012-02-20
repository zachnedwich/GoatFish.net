using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

namespace GoatFish.net
{
    public class Models
    {
        private static SQLiteConnection _connection;
        private static SQLiteCommand _command;
        private const string ConnectionString = @"Data Source=test.db;Version=3;";
        public Models()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _connection = new SQLiteConnection(ConnectionString);
            OpenConnection();
            _command =
                new SQLiteCommand(
                    "CREATE TABLE IF NOT EXISTS Models ( 'id' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 'uuid' TEXT NOT NULL, 'data' BLOB NOT NULL)",
                    _connection);
            _command.ExecuteNonQuery();
            _command =
                new SQLiteCommand("CREATE UNIQUE INDEX IF NOT EXISTS Models_uuid_index on Models (uuid ASC)",
                                  _connection);
        }

        private static void OpenConnection()
        {
            _connection.Open();
        }

        public static bool IsOpen()
        {
            return _connection.State == ConnectionState.Open;
        }


        public static KeyValuePair<string, string> Find(string uuid)
        {
            
            _command = new SQLiteCommand("SELECT * From Models where uuid='" + uuid + "'", _connection);
            var reader = _command.ExecuteReader();

            while (reader.Read())
            {
                var data = new KeyValuePair<string, string>(uuid, ByteArrayToString((byte[]) reader["data"]));
                return data;
            }
            return new KeyValuePair<string, string>("empty", "empty");
        }

    

        public static IDictionary<string, string> Find()
        {
            _command = new SQLiteCommand("SELECT * From Models", _connection);

            var dictionary = new Dictionary<string, string>();
            var reader = _command.ExecuteReader();
            while (reader.Read())
            {
                dictionary.Add(reader["uuid"] as string, ByteArrayToString((byte[]) reader["data"]));
            }
            return dictionary;
        }

        private static string ByteArrayToString(byte[] o)
        {
            var enc = new UTF8Encoding();
            return enc.GetString(o);
        }

        public static void Save(KeyValuePair<string, string> entity)
        {
            _command = entity.Key.Equals(Find(entity.Key).Key)
                           ? new SQLiteCommand(
                                 "UPDATE Models SET data = '" + entity.Value + "' WHERE uuid = '" + entity.Key +
                                 "'", _connection)
                           : new SQLiteCommand(
                                 "INSERT INTO Models ('uuid', 'data') VALUES('" + entity.Key + "','" + entity.Value +
                                 "')", _connection);
            _command.ExecuteNonQuery();
        }

        public static void Save(string key, string value)
        {
            Save(new KeyValuePair<string, string>(key, value));
        }

        public static void Delete(string uuid)
        {
            if (Find(uuid).Equals(new KeyValuePair<string, string>("empty", "empty"))) throw new GoatFishSyntaxException("Entity does not exist.");

            _command = new SQLiteCommand("DELETE FROM Models WHERE uuid = '" + uuid + "'", _connection);
            _command.ExecuteNonQuery();
        }

        public static void Clear()
        {
            _command = new SQLiteCommand("DELETE FROM Models", _connection);
            _command.ExecuteNonQuery();
        }

        public static string GetDbName()
        {
            return _connection.Database;
        }
        public static string GetVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

    }
   
    public class GoatFishSyntaxException : Exception
    {
     
        public GoatFishSyntaxException(string message) : base(message)
        {
            
        }
    }


    public class JSONSerializer
    {
        public static string ToJSON<T>(T list)
        {
            var jSerializer = new JavaScriptSerializer();
            return jSerializer.Serialize(list);
        }

        public static T Deserialize<T>(string json)
        {
            var obj = Activator.CreateInstance<T>();
            var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            var serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }


    }
}