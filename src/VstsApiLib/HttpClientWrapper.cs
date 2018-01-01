using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Devpro.VstsClient.VstsApiLib
{
    public class HttpClientWrapper : IDisposable
    {
        #region Members & Constructor

        private HttpClient _httpClient;
        private readonly ILogger _logger;

        public HttpClientWrapper(IConfigurationService configurationService, ILogger<HttpClientWrapper> logger)
        {
            _logger = logger;
            _logger.LogDebug("Creating a new instance of HttpClientWrapper");

            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "", configurationService.PersonalAccessToken)))
            );
        }

        #endregion

        #region HttpClient Methods

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _httpClient.GetAsync(requestUri);
        }

        public async Task<Stream> GetStreamAsync(string requestUri)
        {
            return await _httpClient.GetStreamAsync(requestUri);
        }

        #endregion

        #region IDisposable Method

        public void Dispose()
        {
            if (_logger != null)
                _logger.LogDebug("Dispose called on HttpClientWrapper instance");
            if (_httpClient != null)
            {
                _httpClient.Dispose();
                if (_logger != null)
                    _logger.LogDebug("Dispose done on HttpClientWrapper instance");
            }
        }

        #endregion
    }
}
