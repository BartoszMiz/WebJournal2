using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebJournal2.Web.Core.Services
{
	public class AuthenticationService
	{
		private readonly ApiRequestService requestService;
		private string token;
		public string Token => token;
		private bool isAuthenticated;
		public bool IsAuthenticated => isAuthenticated;

		public AuthenticationService(ApiRequestService requestService)
		{
			this.requestService = requestService;
			token = string.Empty;
		}

		public async Task<bool> Authenticate(string password)
		{
			string receivedToken;
			try
			{
				receivedToken = await requestService.AuthenticateAsync(password);
			}
			catch(HttpRequestException)
			{
				return false;
			}

			if (receivedToken == null)
			{
				return false;
			}
			else
			{
				token = receivedToken;
				isAuthenticated = true;
				requestService.http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				return true;
			}
		}

		public void Deauthenticate()
		{
			isAuthenticated = false;
			token = string.Empty;
		}
	}
}
