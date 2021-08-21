using System;
using System.Threading.Tasks;

namespace ProjectEuler.Client
{
    /// <summary>
    /// Provides methods for retrieving project euler html pages
    /// </summary>
    public interface IProjectEulerProblemProvider
    {
        Task<ProblemDetails> ProblemDetailsFor(int problemNumber);
    }
}
