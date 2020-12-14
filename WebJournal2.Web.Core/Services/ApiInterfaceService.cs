using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebJournal2.Web.Core.Services
{
	public class ApiInterfaceService
	{
		private readonly HttpClient http;
		private const string baseApiUrl = "http://localhost:1111/api";

		public ApiInterfaceService(HttpClient http)
		{
			this.http = http;
		}

		public async Task<JournalEntry[]> GetEntriesAsync()
		{
			var resp = await http.GetAsync(baseApiUrl + "/entries");
			if (!resp.IsSuccessStatusCode)
				return null;
			var stream = await resp.Content.ReadAsStreamAsync();
			return await JsonSerializer.DeserializeAsync<JournalEntry[]>(stream);
		}

		public async Task<JournalEntry> GetEntry(uint id)
		{
			var resp = await http.GetAsync(baseApiUrl + $"/entries/{id}");
			if (!resp.IsSuccessStatusCode)
				return null;
			var stream = await resp.Content.ReadAsStreamAsync();
			return await JsonSerializer.DeserializeAsync<JournalEntry>(stream);
		}
	}
}
