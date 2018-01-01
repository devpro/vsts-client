using Devpro.VstsClient.VstsApiLib;

namespace Devpro.VstsClient.ConsoleApp
{
    class ConfigurationService : IConfigurationService
    {
        public string PersonalAccessToken { get; private set; }

        public ConfigurationService(string personalAccessToken)
        {
            PersonalAccessToken = personalAccessToken;
        }
    }
}
