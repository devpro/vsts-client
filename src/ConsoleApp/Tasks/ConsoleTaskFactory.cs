using Microsoft.Extensions.DependencyInjection;

namespace Devpro.VstsClient.ConsoleApp.Tasks
{
    /// <summary>
    /// Console task factory to create at runtime the task given the user input.
    /// </summary>
    class ConsoleTaskFactory
    {
        /// <summary>
        /// Create a ITask given a specific key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static ITask CreateTask(string key, ServiceProvider serviceProvider)
        {
            switch(key)
            {
                case IterationsTask.ArgumentName:
                    return new IterationsTask(serviceProvider);
                default:
                    return null;
            }
        }
    }
}
