﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Capstone.DAO;
using Capstone.Security;
using Capstone.Data;
using Microsoft.EntityFrameworkCore;
using Capstone.Models;
using Capstone.Services;
using Capstone.DAO.Reference;
using Capstone.DAO.Position.Quarterback;
using Capstone.Services.Position;


namespace Capstone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            string connectionString = Configuration.GetConnectionString("Project");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddScoped<FantasyDataService>();
            services.AddScoped<ScoreService>();
            services.AddScoped<PlayerStatsExtService>();
            services.AddScoped<QBService>();
            services.AddTransient<ITeamDao, TeamSqlDao>();
            services.AddTransient<IPlayerDao, PlayerSqlDao>();
            services.AddTransient<IFantasyRosterDao, FantasyRosterSqlDao>();
            services.AddTransient<IRosterPlayerDao, RosterPlayerSqlDao>();
            services.AddTransient<IFantasyLineupDao, FantasyLineupSqlDao>();
            services.AddTransient<ILineupPlayerDao, LineupPlayerSqlDao>();   
            services.AddTransient<IPlayerStatsDao, PlayerStatsSqlDao>();
            services.AddTransient<IConfigurationDao, ConfigurationSqlDao>();
            services.AddTransient<IQBSeasonTotalDao, QBSeasonTotalSqlDao>();
            services.AddTransient<IQBSeasonAverageDao, QBSeasonAverageSqlDao>();
            services.AddTransient<IQBLast4TotalDao, QBLast4TotalSqlDao>();
            services.AddTransient<IQBLast4AverageDao, QBLast4AverageSqlDao>();
            services.AddTransient<IQBWeeklyTotalDao, QBWeeklyTotalSqlDao>();
            services.AddTransient<IQBWeeklyProjectedDao, QBWeeklyProjectedSqlDao>();

            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(Configuration["JwtSecret"]);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub] = "sub";
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    NameClaimType = "name"
                };
            });

            // Dependency Injection configuration
            services.AddSingleton<ITokenGenerator>(tk => new JwtGenerator(Configuration["JwtSecret"]));
            services.AddSingleton<IPasswordHasher>(ph => new PasswordHasher());
            services.AddTransient<IUserDao>(m => new UserSqlDao(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
