using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotkeyLib_Auth_Server.Function.Generator
{
    public static class TokenGenerator
    {
        public static string GetToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
