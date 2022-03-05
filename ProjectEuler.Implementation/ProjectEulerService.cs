using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ProjectEuler.Implementation
{
  /// <summary>
  /// Retrieves Project Euler HTML pages from the https://projecteuler.net/ web site
  /// </summary>
  class ProjectEulerService : IProjectEulerService
  {
    public const string PROJECT_EULER_URL = "https://projecteuler.net/";
    public Uri ProblemUrlPattern => new(ProjectEulerUri, "/problem={0}");
    private readonly string _projectEulerUrl;
    private Uri ProjectEulerUri => new(_projectEulerUrl);
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Construct new project euler api.
    /// </summary>
    /// <param name="httpClient">Http client.</param>
    /// <param name="projectEulerUri">Project euler site url. Defaults to "https://projecteuler.net/".</param>
    public ProjectEulerService(HttpClient httpClient, string projectEulerUri = PROJECT_EULER_URL)
    {
      _httpClient = httpClient;
      _projectEulerUrl = projectEulerUri;
    }

    public async Task<ProblemDetailsDto> GetDetailsForAsync(int problemNumber)
    {
      var problemUrl = String.Format(ProblemUrlPattern.ToString(), problemNumber);
      var problemHtml = await Query(problemUrl).ConfigureAwait(false);
      var document = new HtmlDocument();
      document.LoadHtml(problemHtml);
      var title = ParseProblemTitle(document);
      var details = ParseDetailsHtml(document);
      return new ProblemDetailsDto(problemUrl, problemNumber, title, details);
    }

    private static string ParseProblemTitle(HtmlDocument document)
    {
      var node = document.GetElementbyId("content");
      foreach (var childNode in node.ChildNodes)
      {
        if (childNode.Name.Equals("h2", StringComparison.InvariantCultureIgnoreCase))
          return childNode.InnerText;
      }
      return null;
    }

    private static string ParseDetailsHtml(HtmlDocument document)
    {
      var node = document.GetElementbyId("content");
      foreach (var childNode in node.ChildNodes)
      {
        if (childNode.HasClass("problem_content"))
          return childNode.InnerHtml;
      }
      return null;
    }

    private async Task<string> Query(string url)
    {
      var result = await this._httpClient.GetAsync(url).ConfigureAwait(false);
      if (result.IsSuccessStatusCode)
        return await result.Content.ReadAsStringAsync().ConfigureAwait(false);
      else
        throw new InvalidOperationException($"Query '{url}' failed with status code : {result.StatusCode}");
    }
  }
}
