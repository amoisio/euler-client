using System.Threading.Tasks;
using ProjectEuler.Implementation;

namespace ProjectEuler.Tests
{
  class MockService : IProjectEulerService
  {
    private readonly string _ref;
    private readonly string _title;
    private readonly string _html;
    public MockService(string @ref, string title, string html)
    {
      _ref = @ref;
      _title = title;
      _html = html;
    }

    public Task<ProblemDetailsDto> GetDetailsForAsync(int problemNumber)
    {
      var dto = new ProblemDetailsDto(_ref, problemNumber, _title, _html);
      return Task.FromResult(dto);
    }
  }
}
