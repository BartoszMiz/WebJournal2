using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebJournal2.Web.Core.Services;

namespace WebJournal2.Web
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

			builder.Services.AddScoped<NavigationService>();
			builder.Services.AddScoped<AuthTokenHolder>();
			builder.Services.AddScoped<ApiRequestService>();
			builder.Services.AddScoped<JournalEntryService>();
			builder.Services.AddScoped<AuthenticationService>();

			await builder.Build().RunAsync();
		}
	}
}
