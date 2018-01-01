using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Devpro.VstsClient.ConsoleApp
{
    /// <summary>
    /// Main program of VSTS client console project.
    /// </summary>
    class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();

            // wait one second for educational purpose only (see resources getting disposed)
            System.Threading.Thread.Sleep(1000);
        }

        public static async Task MainAsync(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to VSTS client console by Devpro!");

                // TODO: check args.Length > 0 and other args, display usage if invalid

                using (var serviceProvider = BuildDependencyInjection(args))
                {
                    var task = Tasks.ConsoleTaskFactory.CreateTask(args[0], serviceProvider);
                    // TODO: check task is not null, display usage if null

                    await task.ExecuteAsync(args);
                }

                Console.WriteLine("Bye");
            }
            catch (Exception exc)
            {
                Console.WriteLine($"An error occured: \"{0}\"", exc.Message);
                // TODO: log much more! look into inner exceptions
            }
        }

        /// <summary>
        /// Build service provider (dependency injection)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static ServiceProvider BuildDependencyInjection(string[] args)
        {
            var services = new ServiceCollection();
            // add logging
            services.AddSingleton(new LoggerFactory()
                .AddConsole(LogLevel.Debug)
                .AddDebug());
            services.AddLogging();
            // add project deficitions
            services.AddTransient<VstsApiLib.IConfigurationService>(x => new ConfigurationService(args[3])); // TODO: to be improved
            services.AddScoped<VstsApiLib.HttpClientWrapper>();
            services.AddTransient<VstsApiLib.IterationService>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
