using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Devpro.VstsClient.VstsApiLib.Dto;
using Microsoft.Extensions.Logging;

namespace Devpro.VstsClient.VstsApiLib
{
    public class BuildService
    {
        private ILogger _logger;

        public BuildService(ILogger<HttpClientWrapper> logger)
        {
            _logger = logger;
        }

        public async Task<BuildFindResultDto> GetBuilds(string vstsAccountName, string projectName, string personalaccesstoken)
        {
            //TODO: greatly improve this code!
            return await GetAsync(vstsAccountName, projectName, personalaccesstoken, "build/builds");
        }

        public virtual async Task<BuildFindResultDto> GetAsync(string vstsAccountName, string projectName, string personalaccesstoken, string action)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "", personalaccesstoken)))
                    );

                    //using (var response = await client.GetAsync($"https://{vstsAccountName}.visualstudio.com/DefaultCollection/{projectName}/_apis/{action}"))
                    //{
                    //    response.EnsureSuccessStatusCode();
                    //    //TODO: get a class instead of a string
                    //    var responseBody = await response.Content.ReadAsStringAsync();
                    //    return responseBody;
                    //}

                    // TODO: rework!!!!
                    var requestUri = $"https://{vstsAccountName}.visualstudio.com/DefaultCollection/{projectName}/_apis/{action}";
                    var serializer = new DataContractJsonSerializer(typeof(BuildFindResultDto));
                    var streamTask = client.GetStreamAsync(requestUri);
                    var output = serializer.ReadObject(await streamTask) as BuildFindResultDto;
                    return output;
                }
            }
            catch (Exception exc)
            {
                _logger.LogCritical($"An exception occured: { exc.Message }");
                throw;
            }
        }
    }
}
