using System.IO;
using System.Runtime.InteropServices;

namespace HotkeyLib
{
    public static class Auth
    {
        [DllExport("HotkeyLib_Auth_Login", CallingConvention.StdCall)]
        public static int Login(string id, string pw)
        {
            File.AppendAllText("./log.txt", $"[Login] id : {id} pw : {pw}\r\n");

            Data.Token = id + pw;
            return 0;
        }

        [DllExport("HotkeyLib_Auth_CheckLogin", CallingConvention.StdCall)]
        public static int CheckLogin()
        {
            File.AppendAllText("./log.txt", $"[CheckLogin] Login State : {Data.Token == "-1"}\r\n");

            return Data.Token == "-1" ? 0 : 1;
        }


        [DllExport("HotkeyLib_Auth_CheckToken", CallingConvention.StdCall)]
        public static string CheckToken()
        {
            File.AppendAllText("./log.txt", $"[CheckToken] Token : {Data.Token}\r\n");

            return Data.Token;
        }
    }
}