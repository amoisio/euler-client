using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using ProjectEuler.Api;

namespace ProjectEuler.Cli
{
  class Program
  {
    static async Task<int> Main(string[] args)
    {
      var arguments = ParseArguments(args);

      using var client = new HttpClient();
      var pe = new ProjectEuler.Implementation.ProjectEuler(client);
      var problem = await pe.GetProblemAsync(arguments.ProblemNumber);

      Print(problem);

      using var templateStream = File.OpenRead(arguments.TemplatePath);
      using var outputStream = File.Open(arguments.OutputPath, arguments.Mode);
      await problem.GenerateAsync(templateStream, outputStream);
      await outputStream.FlushAsync();

      return 1;
    }

    private static Arguments ParseArguments(string[] args)
    {
      if (args.Length < 2) 
      {
        throw new ArgumentException("At least problem number and template path must be provided.");
      }

      var arguments = new Arguments
      {
        ProblemNumber = Int32.Parse(args[0]),
        TemplatePath = args[1],
        OutputPath = args.Length > 2 ? args[2] : ".",
        OverrideExisting = args.Length > 3 && Boolean.Parse(args[3])
      };
      return arguments;
    }

    private static void Print(IProblem problem) {
      System.Console.WriteLine(problem.Ref);
      System.Console.WriteLine(problem.Title);
      System.Console.WriteLine(problem.Number);
      System.Console.WriteLine(problem.DescriptionHtml);
    }
  }

  class Arguments
  {
    public int ProblemNumber { get; set; }
    public string TemplatePath { get; set; }
    public string OutputPath { get; set; }
    public bool OverrideExisting { get; set; } 
    public FileMode Mode => OverrideExisting ? FileMode.Create : FileMode.CreateNew;
  }
}
