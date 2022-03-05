using System.Threading.Tasks;
using System.IO;

namespace ProjectEuler.Api
{
  public interface IProblem
  {
    /// <summary>
    /// Unique reference - the host relative uri of the problem such as "/problem=123"
    /// </summary>
    string Ref { get; }

    /// <summary>
    /// Number of the problem
    /// </summary>
    int Number { get; }

    /// <summary>
    /// Problem title
    /// </summary>
    string Title { get; }

    /// <summary>
    /// HTML problem description statement
    /// </summary>
    string DescriptionHtml { get; }

    /// <summary>
    /// Generate a problem file based on the given template.
    /// </summary>
    /// <param name="templateStream">Template file stream</param>
    /// <param name="outputStream">Generated output file stream</param>
    Task GenerateAsync(Stream templateStream, Stream outputStream);
  }
}
