using System;
using System.Collections.Generic;
using HotkeyLib_Auth_Server.Function;
using HotkeyLib_Auth_Server.Function.Generator;
using Microsoft.AspNetCore.Mvc;

namespace HotkeyLib_Auth_Server.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get(LoginModel data)
        {
            var user = Program.DataBase.UserCollection.FindById(data.Id);

            if (user == null) return NotFound();
            if (user.Password != HashGenerator.ComputeSha512Hash(data.Password + user.Salt, 100)) return Unauthorized();

            user.Token = TokenGenerator.GetToken();
            user.LastLoginDate = DateTime.UtcNow.AddHours(9);

            Program.DataBase.UserCollection.Update(user);

            var jwtData = new Dictionary<string, string>
            {
                {"Id", user.Id},
                {"Name", user.Name}
            };

            return Jwt.NewJwt(user.Token, jwtData);
        }

        public class LoginModel
        {
            [FromQuery(Name = "Id")] public string Id { get; set; }
            [FromQuery(Name = "Password")] public string Password { get; set; }
        }
    }
}