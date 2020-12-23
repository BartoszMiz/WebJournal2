using System.Net.Http;
using System.Threading.Tasks;

namespace WebJournal2.Web.Core.Services
{
	public class AuthenticationService
	{
		private readonly ApiRequestService requestService;
		private readonly AuthTokenHolder authToken;
		private bool isAuthenticated = false;
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
				receivedToken = await requestService.Authenticate(password);
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
