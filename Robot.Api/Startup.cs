using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Robot.Contract;
using Robot.Handler;
using Robot.Model;
using Robot.Repository;
using Robot.Service;
using Swashbuckle.AspNetCore.Swagger;

namespace Robot.API
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
            services
             .AddDbContext<RobotDbContext>(options => options.UseInMemoryDatabase(databaseName: "Robot"))
             .AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Warning))
             .AddScoped<IMoveCommand, MoveCommand>()
             .AddScoped<ILeftCommand, LeftCommand>()
             .AddScoped<IRightCommand, RightCommand>()
             .AddScoped<IPlaceCommand, PlaceCommand>()
             .AddScoped<IReportCommand, ReportCommand>()
             .AddScoped<ICommandService, CommandService>()
             .AddScoped<IRepository<Coordinate>, Repository<Coordinate>>()
             .AddScoped<IRepository<ReportLog>, Repository<ReportLog>>()
             .AddScoped<IPositionSettingRepository, PositionSettingRepository>()
             .AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        Constants.Project.VERSIONNAME,
                        new Info { Title = Constants.Project.PROJECTNAME, Version = Constants.Project.VERSIONNAME, Description = Constants.Project.PROJECTDESCRIPTION, TermsOfService = "None" });

                    var fileName = this.GetType().GetTypeInfo().Module.Name.Replace(".dll", ".xml").Replace(".exe", ".xml");
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));
                    c.DescribeAllEnumsAsStrings();
                });
            
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{Constants.Project.VERSIONNAME}/swagger.json", $"{Constants.Project.PROJECTNAME} {Constants.Project.VERSIONNAME}");
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
             .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<RobotDbContext>().Database.EnsureCreated(); 
            }
        }
    }
}
