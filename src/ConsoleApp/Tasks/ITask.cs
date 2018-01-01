using System.Threading.Tasks;

namespace Devpro.VstsClient.ConsoleApp.Tasks
{
    interface ITask
    {
        Task ExecuteAsync(string[] args);
    }
}
