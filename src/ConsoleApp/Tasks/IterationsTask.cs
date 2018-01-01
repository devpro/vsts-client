using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Devpro.VstsClient.VstsApiLib;
using Devpro.VstsClient.VstsApiLib.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace Devpro.VstsClient.ConsoleApp.Tasks
{
    /// <summary>
    /// Execute a task on VSTS Iterations (Agile methodology).
    /// </summary>
    class IterationsTask : ITask
    {
        private ServiceProvider _serviceProvider;

        public const string ArgumentName = "iterations";

        public IterationsTask(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Query VSTS to get all iterations and display them as JSON in the console.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(string[] args)
        {
            var service = _serviceProvider.GetRequiredService<IterationService>();

            // TODO: check argument validity, display usage if invalid

            var output = await service.FindAllAsync(args[1], args[2], args[3]);

            var serializer = new DataContractJsonSerializer(typeof(IterationFindResultDto));
            var ms = new MemoryStream();
            serializer.WriteObject(ms, output);
            Console.WriteLine(Encoding.UTF8.GetString(ms.ToArray()));
        }
    }
}
