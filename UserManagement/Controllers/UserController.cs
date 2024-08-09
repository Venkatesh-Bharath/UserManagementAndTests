using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Repository;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserInterface userInterface;
        public UserController(IUserInterface _userInterface)
        {
            userInterface = _userInterface;
        }
        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult>  Create(User user)
        {
            var result= await userInterface.CreateAsync(user);
            if (result)
            {
                return CreatedAtAction(nameof(Create), new { id = user.id }, user);
            }
            else
            {
                return BadRequest();
            }
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var result = await userInterface. GetAllAsync();
            if (!result.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }

        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await userInterface.GetByIdAsync(id);
            if (result is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

       

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            user.id= id;
            var result = await userInterface.UpdateAsync(user);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await userInterface.DeleteAsync(id);
             if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
