using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebJournal2.Web.Core.Services
{
	public class AuthenticationService
	{
		private readonly ApiRequestService requestService;
		private readonly AuthTokenHolder authToken;
		private bool isAuthenticated;
		public bool IsAuthenticated => isAuthenticated;

		public AuthenticationService(ApiRequestService requestService, AuthTokenHolder authToken)
		{
			this.requestService = requestService;
			this.authToken = authToken;
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
				authToken.Token = receivedToken;
				isAuthenticated = true;
				requestService.http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Token);
				return true;
			}
		}

		public void Deauthenticate()
		{
			isAuthenticated = false;
			authToken.Token = string.Empty;
		}
	}
}
