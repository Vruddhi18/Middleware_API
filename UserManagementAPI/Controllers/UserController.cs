using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    
    // GET: api/user
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(Models.User.Users.Values);
    }

    // GET: api/user/{id}
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        if (Models.User.Users.TryGetValue(id, out var user))
        {
            return Ok(user);
        }
        return NotFound();
    }

    // POST: api/user
    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User newUser)
    {
        int newId = Models.User.Users.Keys.Max() + 1;
        Models.User.Users[newId] = newUser;
        return CreatedAtAction(nameof(GetUser), new { id = newId }, newUser);
    }

    // PUT: api/user/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        if (Models.User.Users.ContainsKey(id))
        {
            Models.User.Users[id] = updatedUser;
            return NoContent();
        }
        return NotFound();
    }

    // DELETE: api/user/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        if (Models.User.Users.Remove(id))
        {
            return NoContent();
        }
        return NotFound();
    }
}