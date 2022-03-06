using System.Threading.Tasks;
using CliFx;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectEuler.Cli
{
  public static class Program
  {
    static async Task<int> Main()
    {
      var services = new ServiceCollection();

      services.AddHttpClient<ProjectEuler.Implementation.ProjectEuler>();
      services.AddTransient<MainCommand>();

      var serviceProvider = services.BuildServiceProvider();
      var returnCode = await new CliApplicationBuilder()
          .AddCommandsFromThisAssembly()
          .UseTypeActivator(serviceProvider.GetService)
          .Build()
          .RunAsync();

      return returnCode;
    }
  }
}
