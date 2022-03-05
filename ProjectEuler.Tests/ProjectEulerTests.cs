using Xunit;
using System.Threading.Tasks;
using System.IO;

namespace ProjectEuler.Tests
{
  public class ProblemTests
  {
    [Fact]
    public async Task ShouldGenerateTemplatedOutput()
    {
      var number = 51523;
      var problemRef = $"/problem={number}";
      var title = "problem title";
      var html = "<p>Hello world</p>";
      var service = new MockService(problemRef, title, html);
      var pe = new ProjectEuler.Implementation.ProjectEuler(service);
      var problem = await pe.GetProblemAsync(number);

      using var templateStream = new MemoryStream();
      using var writer = new StreamWriter(templateStream);
      await writer.WriteLineAsync("Hello ##Title##");
      await writer.WriteLineAsync("Problem ##No##");
      await writer.WriteLineAsync("##Details##");
      await writer.WriteLineAsync("Ref ##Ref##");
      await writer.FlushAsync();
      templateStream.Position = 0;

      using var outputStream = new MemoryStream();
      await problem.GenerateAsync(templateStream, outputStream);

      using var reader = new StreamReader(outputStream);     
      
      var titleLine = await reader.ReadLineAsync();
      Assert.Equal($"Hello {title}", titleLine);
      var numberLine = await reader.ReadLineAsync();
      Assert.Equal($"Problem {number}", numberLine);
      var details = await reader.ReadLineAsync();
      Assert.Equal(html, details);
      var refLine = await reader.ReadLineAsync();
      Assert.Equal($"Ref {problemRef}", refLine);
    }
  }

  public class ProjectEulerTests
  {
    [Fact]
    public async Task ShouldGetProblemDetails()
    {
      var number = 51523;
      var problemRef = $"/problem={number}";
      var title = "problem title";
      var html = "<p>Hello world</p>";
      var service = new MockService(problemRef, title, html);
      var pe = new ProjectEuler.Implementation.ProjectEuler(service);

      var problem = await pe.GetProblemAsync(number);

      Assert.Equal(number, problem.Number);
      Assert.Equal(problemRef, problem.Ref);
      Assert.Equal(title, problem.Title);
      Assert.Equal(html, problem.DescriptionHtml);
    }
  }
}
