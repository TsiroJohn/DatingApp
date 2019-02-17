using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository _repository;
        public AuthController (IAuthRepository repository) {
            this._repository = repository;
        }

        [HttpPost ("register")]

        public async Task<IActionResult> Register (UserForRegisterDto userForRegisterDto) {
            // validate request

            var username = userForRegisterDto.Username.ToLower ();
            if (await _repository.UserExists (username))
                return BadRequest ("Username already exists");

            var userToCreate = new User {
                Username = username
            };

            var createdUser = await _repository.Register (userToCreate, userForRegisterDto.Password);

            return StatusCode (201);
        }
    }
}