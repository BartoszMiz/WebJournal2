using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebJournal2.API.Services;
using WebJournal2.API.Core.Models;

namespace WebJournal2.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class EntriesController : ControllerBase
	{
		private readonly EntryService entryService;

		public EntriesController(EntryService entryService)
		{
			this.entryService = entryService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await entryService.GetEntriesAsync().ConfigureAwait(false));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(uint id)
		{
			var entry = await entryService.GetEntryAsync(id).ConfigureAwait(false);
			if (entry == null)
				return NotFound($"Entry with id {id} does not exist!");
			return Ok(entry);
		}

		[HttpGet("lastid")]
		public async Task<IActionResult> GetLastId()
		{
			uint lastId = (await entryService.GetEntriesAsync().ConfigureAwait(false)).Last().Id;
			return Ok(lastId);
		}

		[HttpGet("{first}:{last}")]
		public async Task<IActionResult> GetEntriesRange(uint first, uint last)
		{
			var entries = await entryService.GetEntriesAsync().ConfigureAwait(false);
			var entryRange = entries.Where(x => x.Id >= first && x.Id <= last).ToArray();
			return Ok(entryRange);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] JournalEntry entry)
		{
			return Ok(await entryService.AddEntryAsync(entry).ConfigureAwait(false));
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] JournalEntry entry)
		{
			var updatedEntry = await entryService.UpdateEntryAsync(entry).ConfigureAwait(false);
			if(updatedEntry == null)
				return NotFound($"Entry with id {entry.Id} does not exist!");
			return Ok(updatedEntry);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(uint id)
		{
			var deletedEntry = await entryService.DeleteEntryAsync(id).ConfigureAwait(false);
			if(deletedEntry == null)
				return NotFound($"Entry with id {id} does not exist!");
			return Ok(deletedEntry);
		}
	}
}
