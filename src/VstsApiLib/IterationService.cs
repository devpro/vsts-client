using System;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Devpro.VstsClient.VstsApiLib.Dto;
using Microsoft.Extensions.Logging;

namespace Devpro.VstsClient.VstsApiLib
{
    /// <summary>
    /// Provide a service to access and manage Iterations in VSTS.
    /// <see cref="https://www.visualstudio.com/en-us/docs/integrate/api/work/iterations"/>
    /// </summary>
    public class IterationService
    {
        private readonly HttpClientWrapper _client;
        private readonly ILogger _logger;

        public string ApiVersion { get; set; } = "v2.0-preview";

        public IterationService(HttpClientWrapper client, ILogger<IterationService> logger)
        {
            _client = client;
            _logger = logger;
        }

        /// <summary>
        /// Get all iterations.
        /// GET https://{instance}/DefaultCollection/{project}/{team}/_apis/work/TeamSettings/Iterations?[$timeframe=current&]api-version={version}
        /// </summary>
        /// <param name="vstsAccountName"></param>
        /// <param name="projectName"></param>
        /// <param name="personalaccesstoken"></param>
        /// <returns>Result DTO</returns>
        public async Task<IterationFindResultDto> FindAllAsync(string vstsAccountName, string projectName, string personalaccesstoken)
        {
            _logger.LogDebug($"Find all iterations for {{ account: {vstsAccountName}, projectName: {projectName} }}");
            var requestUri = $"https://{vstsAccountName}.visualstudio.com/DefaultCollection/{projectName}/_apis/work/TeamSettings/Iterations?api-version={ApiVersion}";
            var result =  await GetAsync<IterationFindResultDto>(requestUri, personalaccesstoken);
            _logger.LogDebug($"Number of iterations found: {result.count}");
            return result;
        }

        // TODO: implement POST https://{instance}/DefaultCollection/{project}/{team}/_apis/work/TeamSettings/Iterations?api-version={version}

        // TODO: make this methods generic to multiple services

        public virtual async Task<string> GetAsync(string requestUri, string personalaccesstoken)
        {
            try
            {
                using (var response = await _client.GetAsync(requestUri))
                {
                    response.EnsureSuccessStatusCode();
                    //TODO: get a class instead of a string
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
            }
            catch (Exception exc)
            {
                _logger.LogError($"An error occured: {exc.Message}");
                throw;
            }
        }

        public virtual async Task<T> GetAsync<T>(string requestUri, string personalaccesstoken)
            where T : class
        {
            try
            {
                // TODO: should we create something specific in memory to store the serializer everytime we ask it for a new type?
                var serializer = new DataContractJsonSerializer(typeof(T));
                var streamTask = _client.GetStreamAsync(requestUri);
                var output = serializer.ReadObject(await streamTask) as T;
                return output;
            }
            catch (Exception exc)
            {
                _logger.LogError($"An error occured: {exc.Message}");
                throw;
            }
        }
    }
}
