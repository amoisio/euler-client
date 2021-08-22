using System;
using System.Threading.Tasks;

namespace ProjectEuler.Client
{
    /// <summary>
    /// Provides methods for interacting with the project euler.
    /// </summary>
    public interface IProjectEulerAPI
    {
        Task<ProblemDetails> ProblemDetailsFor(int problemNumber);
    }
}
