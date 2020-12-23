using System.Net.Http;
using System.Threading.Tasks;

namespace WebJournal2.Web.Core.Services
{
	public class AuthenticationService
	{
		private readonly ApiRequestService requestService;
		private bool isAuthenticated = false;
		public bool IsAuthenticated => isAuthenticated;
		private string token;
		public string Token => token;

		public AuthenticationService(ApiRequestService requestService)
		{
			this.requestService = requestService;
		}

		public async Task<bool> Authenticate(string password)
		{
			string receivedToken = string.Empty;
			try
			{
				receivedToken = await requestService.Authenticate(password);
			}
			catch(HttpRequestException)
			{
				// For development only
				// If api unavailable authenticate the user regardless of the password
				token = string.Empty;
				isAuthenticated = true;
			}

			if (receivedToken == null)
			{
				return false;
			}
			else
			{
				token = receivedToken;
				isAuthenticated = true;
				return true;
			}
		}
	}
}
