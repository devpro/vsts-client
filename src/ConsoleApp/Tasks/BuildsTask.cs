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
    internal class BuildsTask : ITask
    {
        private ServiceProvider _serviceProvider;

        public const string ArgumentName = "builds";

        public BuildsTask(ServiceProvider serviceProvider)
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
            var service = _serviceProvider.GetRequiredService<BuildService>();

            // TODO: check argument validity, display usage if invalid

            var output = await service.GetBuilds(args[1], args[2], args[3]);

            var serializer = new DataContractJsonSerializer(typeof(BuildFindResultDto));
            var ms = new MemoryStream();
            serializer.WriteObject(ms, output);
            Console.WriteLine(Encoding.UTF8.GetString(ms.ToArray()));
        }
    }
}
