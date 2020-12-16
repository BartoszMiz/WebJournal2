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
			string recievedToken = await requestService.Authenticate(password);
			if (recievedToken == null)
				return false;
			else
			{
				token = recievedToken;
				return true;
			}
		}
	}
}
