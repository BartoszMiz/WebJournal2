using Microsoft.AspNetCore.Components;

namespace WebJournal2.Web.Core.Services
{
	public class NavigationService
	{
		private readonly NavigationManager nav;

		public NavigationService(NavigationManager nav)
		{
			this.nav = nav;
		}

		public void RefreshPage()
		{
			nav.NavigateTo(nav.Uri);
		}

		public void GoToPage(string url)
		{
			nav.NavigateTo(url);
		}

		public void GoToIndexPage()
		{
			GoToPage("/");
		}

		public void GoToNewEntryPage()
		{
			GoToPage("/new-entry");
		}
	}
}