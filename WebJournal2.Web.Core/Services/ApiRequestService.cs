using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebJournal2.Web.Core.Services
{
	public class ApiRequestService
	{
		private readonly HttpClient http;
		private const string baseApiUrl = "http://localhost:1111/api";

		public ApiRequestService(HttpClient http)
		{
			this.http = http;
		}

		public async Task<string> Authenticate(string password)
		{
			var requestContent = new StringContent(password);
			var resp = await http.PostAsync(baseApiUrl + "/login", requestContent);
			if(resp.IsSuccessStatusCode)
			{
				var content = await resp.Content.ReadAsStreamAsync();
				var deserializedContent = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(content);
				return deserializedContent["token"];
			}
			return null;
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
