using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // For demonstration, we'll use an in-memory list.
        // In a real project, you’d inject a database context instead.
        private static List<User> _users = new List<User>();

        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_users);
        }

        // GET api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/users
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
            // 1. Check ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // 2. Proceed with creation
            newUser.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            newUser.CreatedAt = DateTime.UtcNow;
            _users.Add(newUser);

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        // PUT api/users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            // Check ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound($"User with Id {id} not found.");
            }

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;

            return NoContent();
        }
        // DELETE api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound($"User with Id {id} was not found.");
            }

            _users.Remove(user);
            return NoContent();
        }
        

    }
}
