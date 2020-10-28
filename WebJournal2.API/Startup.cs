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
using System.Data.Common;
using Microsoft.Data.Sqlite;
using WebJournal2.API.Contexts;
using Microsoft.EntityFrameworkCore;

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

			string dbPassword = Configuration["Db:Password"];
			string connectionString = new SqliteConnectionStringBuilder
			{
				DataSource = "app.db",
				Mode = SqliteOpenMode.ReadWriteCreate,
				Password = string.IsNullOrEmpty(dbPassword) ? null : dbPassword
			}.ToString();

			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlite(connectionString, opt =>
					opt.MigrationsAssembly("WebJournal2.Migrations")));

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

			services.AddSingleton<JwtService>();
			services.AddSingleton<AuthenticationService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}
