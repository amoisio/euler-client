using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ProjectEuler.Tests")]
namespace ProjectEuler.Implementation
{
  interface IProjectEulerService
  {
    Task<ProblemDetailsDto> GetDetailsForAsync(int problemNumber);
  }
}
