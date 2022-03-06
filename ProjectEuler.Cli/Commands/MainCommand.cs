using System.IO;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using ProjectEuler.Api;

namespace ProjectEuler.Cli
{
  [Command]
  public class MainCommand : ICommand
  {
    private readonly ProjectEuler.Implementation.ProjectEuler _euler;
    public MainCommand(ProjectEuler.Implementation.ProjectEuler euler)
    { 
      _euler = euler;
    }

    [CommandParameter(0, Name = "problem", Description = "Problem number.")]
    public int ProblemNumber{ get; set; }

    [CommandOption("template", 't', Description = "Template file path")]
    public string TemplatePath { get; set; }

    [CommandOption("output", 'o', Description = "Output file path")]
    public string OutputPath { get; set; }

    public bool HasOutputPath => !string.IsNullOrWhiteSpace(OutputPath);

    [CommandOption("mode", 'm', Description = "True to allow overwriting output file.")]
    public bool AllowOverwrite { get; set; } = false;

    public FileMode Mode => AllowOverwrite ? FileMode.Create : FileMode.CreateNew;

    public async ValueTask ExecuteAsync(IConsole console)
    {
      var problem = await _euler.GetProblemAsync(ProblemNumber);
      if (string.IsNullOrEmpty(TemplatePath)) {
        await PrintProblem(problem, console);
        return;
      }

      using Stream templateStream = File.OpenRead(TemplatePath);
      using Stream outputStream = HasOutputPath
          ? File.Open(OutputPath, Mode)
          : new MemoryStream();
      await problem.GenerateAsync(templateStream, outputStream);
      await outputStream.FlushAsync();

      if (HasOutputPath)
      {
        await console.Output.WriteLineAsync($"Output {OutputPath} has been created.");
      }
      else
      {
        await PrintOutput(outputStream, console);
      }
    }

    private static async Task PrintProblem(IProblem problem, IConsole console) {
      await console.Output.WriteLineAsync($"Problem {problem.Number}. {problem.Title}");
      await console.Output.WriteLineAsync($"Ref: {problem.Ref}");
      await console.Output.WriteLineAsync(problem.DescriptionHtml);
    } 
  
    private static async Task PrintOutput(Stream outputStream, IConsole console) {
      outputStream.Position = 0;
      var reader = new StreamReader(outputStream);
      var output = await reader.ReadToEndAsync();
      await console.Output.WriteLineAsync(output);
    }
  }
}
