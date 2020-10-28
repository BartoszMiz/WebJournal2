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

		public UsersController(UserService userService)
		{
			this.userService = userService;
		}

		[HttpGet] [Authorize]
		public IActionResult Get()
		{
			return Ok(JournalUser.StripLoginData((JournalUser[])userService.GetUsers()));
		}

		[HttpGet("{id}")] [Authorize]
		public async Task<IActionResult> Get(uint id)
		{
			return Ok((await userService.GetUserAsync(id)).StripLoginData());
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] JournalUser user)
		{
			var users = userService.GetUsers();
			if (users.Any(x => x.Username == user.Username))
				return BadRequest();

			var addedUser = await userService.AddUserAsync(user);
			return Ok(addedUser.StripLoginData());
		}

		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(uint id, [FromBody] JournalUser newUser)
		{
			var updatedUser = await userService.UpdateUserAsync(id, newUser);

			if (updatedUser == null)
				return BadRequest();

			return Ok(updatedUser.StripLoginData());
		}

		// DELETE api/<UsersController>/5
		[HttpDelete("{id}")] [Authorize]
		public async Task<IActionResult> Delete(uint id)
		{
			var deletedUser = await userService.DeleteUserAsync(id);

			if (deletedUser == null)
				return BadRequest();

			return Ok(deletedUser.StripLoginData());
		}
	}
}
