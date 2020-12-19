using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System;
using WebJournal2.API.Services;
using Microsoft.Data.Sqlite;
using WebJournal2.API.Core.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace WebJournal2.API
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			string dbPassword = Configuration["Database:Password"];
			string connectionString = new SqliteConnectionStringBuilder
			{
				DataSource = "app.db",
				Mode = SqliteOpenMode.ReadWriteCreate,
				Password = string.IsNullOrEmpty(dbPassword) ? null : dbPassword
			}.ToString();

			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlite(connectionString, opt =>
					opt.MigrationsAssembly("WebJournal2.API.Migrations")));

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = Configuration["Jwt:Issuer"],
						ValidAudience = Configuration["Jwt:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
						ClockSkew = TimeSpan.Zero
					};
				});

			services.AddScoped<JwtService>();
			services.AddScoped<PasswordHashingService>();
			services.AddScoped<PasswordService>();
			services.AddScoped<AuthenticationService>();
			services.AddScoped<EntryService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// Migrate database
			using(var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			using(var db = scope.ServiceProvider.GetService<AppDbContext>())
			{
				Console.Write("Migrating database... ");
				db.Database.Migrate();
				Console.WriteLine("Done!");
			}

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(policy =>
				policy.WithOrigins("http://localhost:2222")
				.AllowAnyMethod()
				.WithHeaders(HeaderNames.ContentType));

			//app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
