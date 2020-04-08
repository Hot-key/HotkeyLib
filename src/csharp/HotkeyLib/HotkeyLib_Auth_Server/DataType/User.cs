using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace HotkeyLib_Auth_Server.DataType
{
    public class User
    {
        [BsonId]
        public string Id { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        public string Salt { get; set; }

        public DateTime LastLoginDate { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
