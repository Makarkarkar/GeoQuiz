using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace GeoQuiz.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController:ControllerBase
{
    static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    IDatabase db = redis.GetDatabase();
    
    [ProducesResponseType(404)]
    [EnableCors] 
    [HttpGet]
    [Route("{userName}")]
    public ActionResult GetGuid(string userName)
    {
        var user = new User
        {
            UserName = userName,
            GUID = Guid.NewGuid().ToString()
        };
        var hash = new HashEntry[] { 
            new("name", user.UserName),
            new("GUID", user.GUID)
        };
        db.HashSet(user.GUID, hash);
        var hashFields = db.HashGetAll(user.GUID);
        Console.WriteLine(String.Join("; ", hashFields));
        return Ok(user);
    }
}