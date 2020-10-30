using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebJournal2.API.Services;
using WebJournal2.Core.Models;

namespace WebJournal2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UserService userService;
		private readonly PasswordHashingService passwordHashingService;

		public UsersController(UserService userService, PasswordHashingService passwordHashingService)
		{
			this.userService = userService;
			this.passwordHashingService = passwordHashingService;
		}

		[HttpGet] [Authorize]
		public IActionResult Get()
		{
			return Ok(userService.GetUsers());
		}

		[HttpGet("{id}")] [Authorize]
		public async Task<IActionResult> Get(uint id)
		{
			return Ok(await userService.GetUserAsync(id).ConfigureAwait(false));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] RegistrationRequest registrationRequest)
		{
			var user = registrationRequest.ToJournalUser();
			var users = userService.GetUsers();
			if (users.Any(x => x.Username == user.Username))
				return BadRequest($"{user.Username} is taken!");

			user.Password = passwordHashingService.HashPassword(user.Password);
			var addedUser = await userService.AddUserAsync(user).ConfigureAwait(false);
			return Ok(addedUser);
		}

		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(uint id, [FromBody] JournalUser newUser)
		{
			var updatedUser = await userService.UpdateUserAsync(id, newUser).ConfigureAwait(false);

			if (updatedUser == null)
				return BadRequest($"User with id {id} does not exist!");

			return Ok(updatedUser);
		}

		// DELETE api/<UsersController>/5
		[HttpDelete("{id}")] [Authorize]
		public async Task<IActionResult> Delete(uint id)
		{
			var deletedUser = await userService.DeleteUserAsync(id).ConfigureAwait(false);

			if (deletedUser == null)
				return BadRequest($"User with id {id} does not exist!");

			return Ok(deletedUser);
		}
	}
}
