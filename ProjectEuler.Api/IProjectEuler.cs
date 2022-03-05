using System.Threading.Tasks;

namespace ProjectEuler.Api
{
  public interface IProjectEuler
  {
    /// <summary>
    /// Get project euler problem.
    /// </summary>
    /// <param name="problemNumber">Problem number</param>
    /// <returns>Project euler problem.</returns>
    Task<IProblem> GetProblemAsync(int problemNumber);
  }
}
