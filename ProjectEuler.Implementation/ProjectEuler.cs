using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ProjectEuler.Api;

[assembly: InternalsVisibleTo("ProjectEuler.Tests")]
namespace ProjectEuler.Implementation
{
  /// <summary>
  /// Project euler problem file generator
  /// </summary>
  public class ProjectEuler : IProjectEuler
  {
    private readonly IProjectEulerService _service;

    /// <summary>
    /// Construct a new project euler generator.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="templateStream">Problem file template stream.</param>
    public ProjectEuler(HttpClient httpClient)
      : this(new ProjectEulerService(httpClient))
    {

    }

    /// <summary>
    /// Construct a new project euler generator.
    /// </summary>
    /// <param name="api">Project euler api for interacting with the project euler site.</param>
    /// <param name="templateStream">Problem file template stream.</param>
    internal ProjectEuler(IProjectEulerService service)
    {
      _service = service;
    }

    public async Task<IProblem> GetProblemAsync(int problemNumber)
    {
      var dto = await _service.GetDetailsForAsync(problemNumber).ConfigureAwait(false);
      return new Problem(dto);
    }
  }
}
