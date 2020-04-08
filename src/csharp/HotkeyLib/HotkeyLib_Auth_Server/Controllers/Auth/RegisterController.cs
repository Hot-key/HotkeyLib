using System;
using HotkeyLib_Auth_Server.DataType;
using HotkeyLib_Auth_Server.Function.Generator;
using Microsoft.AspNetCore.Mvc;

namespace HotkeyLib_Auth_Server.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Post(RegisterModel data)
        {
            if (Program.DataBase.UserCollection.FindById(data.Id) != null) return Conflict();

            var user = new User
            {
                Id = data.Id,
                Name = data.Name,
                Salt = TokenGenerator.GetToken(),
                RegisterDate = DateTime.UtcNow.AddHours(9),
                LastLoginDate = DateTime.UtcNow.AddHours(9)
            };

            user.Password = HashGenerator.ComputeSha512Hash(data.Password + user.Salt, 100);

            Program.DataBase.UserCollection.Insert(user);
            return Ok();
        }
        

        public class RegisterModel
        {
            public string Id { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
        }
    }
}