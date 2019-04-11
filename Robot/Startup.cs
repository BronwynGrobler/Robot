using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Robot.Handler;
using Robot.Model;
using Robot.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
             .AddDbContext<RobotDbContext>(options => options.UseInMemoryDatabase(databaseName: "Robot"))
             .AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Warning))
             .AddScoped<IRobotAction, RobotAction>()
             .AddScoped<IRepository<Coordinate>, Repository<Coordinate>>()
             .AddScoped<IRepository<ReportLog>, Repository<ReportLog>>()
             .AddScoped<IPositionSettingRepository, PositionSettingRepository>()
             .AddScoped<IValidator, Validator>();

        }
    }
}
