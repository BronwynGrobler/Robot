using Microsoft.Extensions.DependencyInjection;
using Robot.Handler;
using Robot.Model;
using Robot.Repository;
using System;

namespace Robot
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            var startup = new Startup();

            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();

            // Seed data
            var movementContext = serviceProvider.GetRequiredService<RobotDbContext>();
            movementContext.Database.EnsureCreated();

            Console.WriteLine("Commands available are:");
            Console.WriteLine("PLACE X,Y,F");
            Console.WriteLine("MOVE");
            Console.WriteLine("LEFT");
            Console.WriteLine("RIGHT");
            Console.WriteLine("REPORT");
            Console.WriteLine("EXIT");

            Run();
        }

        static void Run()
        {
            while (true)
            {
                var input = Console.ReadLine();

                var param = input.Split(new char[] { ',', ' ' });
                var result = Enum.TryParse<ECommand>(param[0].ToUpper(), out ECommand cmd);

                // Additional command added just for ease of use
                if (cmd == ECommand.EXIT)
                {
                    break;
                }

                if (!result)
                {
                    Console.WriteLine("An invalid command was entered.");
                }
                else
                {
                    serviceProvider.GetService<IRobotAction>().Execute(param);
                }
            }
        }
    }
}
