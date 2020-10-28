using Microsoft.AspNetCore.Mvc;

namespace WebJournal2.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EchoController : ControllerBase
	{
		[HttpGet("{message}")]
		public IActionResult Get(string message)
		{
			return Ok(message);
		}
	}
}
