using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotkeyLib_Auth_Server.DataType;
using LiteDB;

namespace HotkeyLib_Auth_Server
{
    public class DataBase : IDisposable
    {
        private LiteDatabase database;
        public ILiteCollection<User> UserCollection;

        public DataBase(string dbName)
        {
            InitDataBase(dbName);
        }

        public void InitDataBase(string dbName)
        {
            database = new LiteDatabase($@"{dbName}.db");
            UserCollection = database.GetCollection<User>("user");
        }

        ~DataBase()
        {
            Dispose();
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
