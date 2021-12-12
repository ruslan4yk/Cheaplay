using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cheaplay.Models;
using Cheaplay.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Cheaplay.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository = new UserRepository();
        [HttpGet("all")]
        public ActionResult<List<User>> GetAll() => _userRepository.GetAll();

        [HttpGet("{id}/profile")]
        public ActionResult<User> GetById(int id) => _userRepository.GetById(id);

        [HttpPost("signup")]
        public IActionResult Create([FromBody] User user)
        {
            if (_userRepository.GetByLogin(user.Login) != null)
                return BadRequest("This login is already used.");
            if (_userRepository.GetByEmail(user.Email) != null)
                return BadRequest("This email is already used.");
            var emailCheckup = new EmailAddressAttribute();
            if (!(emailCheckup.IsValid(user.Email)))
                return BadRequest("Invalid email");
            _userRepository.Create(user);
            return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
        }

        [HttpPatch("{id}/profile")]
        public IActionResult Update(int id, [FromQuery] string newEmail, string newPassword)
        {
            User user = _userRepository.GetById(id);
            if (user == null)
                return NotFound("No such user");
            if (newEmail != null)
            {
                if (_userRepository.GetByEmail(newEmail) != null)
                    return BadRequest("This email is already used.");
                else
                    user.Email = newEmail;
            }

            if (newPassword != null)
            {
                if (newPassword.Length >= 6)
                    user.Password = HashFunction(newPassword, id);
                else
                    return BadRequest("Invalid password");
            }
            
            _userRepository.Update(user);
            return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
        }

        [HttpDelete("{id}/profile")]
        public IActionResult Delete(int id)
        {
            User user = _userRepository.GetById(id);
            if (user == null)
                return NotFound("No such user");

            _userRepository.Remove(user);
            return NoContent();
        }

        [NonAction]
        private string HashFunction(string data, int id)
        {
            string hash = "eleks";
            string result = data + hash + id;
            return result.GetHashCode().ToString();
        }


    }
}
