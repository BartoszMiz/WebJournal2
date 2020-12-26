using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace WebJournal2.Web.Core.Services
{
	public class ApiRequestService
	{
		public readonly HttpClient http;
		private readonly IJSRuntime js;
		private const string baseApiUrl = "https://localhost:1111/api";

		public ApiRequestService(HttpClient http, IJSRuntime js)
		{
			this.http = http;
			this.js = js;
		}

		public async Task<string> AuthenticateAsync(string password)
		{
			var requestContent = new StringContent('"' + password + '"', Encoding.UTF8, "application/json");
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

		public async Task<JournalEntry> GetEntryAsync(uint id)
		{
			var resp = await http.GetAsync(baseApiUrl + $"/entries/{id}");
			if (!resp.IsSuccessStatusCode)
				return null;
			var stream = await resp.Content.ReadAsStreamAsync();
			return await JsonSerializer.DeserializeAsync<JournalEntry>(stream);
		}

		public async Task PostEntryAsync(JournalEntry entry)
		{
			var serializerOptions = new JsonSerializerOptions
			{
				IgnoreNullValues = true
			};
			string entryJson = JsonSerializer.Serialize(entry, serializerOptions);

			var requestContent = new StringContent(entryJson, Encoding.UTF8, "application/json");
			await http.PostAsync(baseApiUrl + "/entries", requestContent);
		}

		public async Task PutEntryAsync(JournalEntry entry)
		{
			string entryJson = JsonSerializer.Serialize(entry);
			var requestContent = new StringContent(entryJson, Encoding.UTF8, "application/json");
			await http.PutAsync(baseApiUrl + "/entries", requestContent);
		}

		public async Task DeleteEntryAsync(uint entryId)
		{
			await http.DeleteAsync($"{baseApiUrl}/entries/{entryId}");
		}
	}
}
