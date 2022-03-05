using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ProjectEuler.Api;

namespace ProjectEuler.Implementation
{
  class Problem : IProblem
  {
    internal Problem(ProblemDetailsDto dto)
    {
      Ref = dto.Ref;
      Number = dto.Number;
      Title = dto.Title;
      DescriptionHtml = dto.DescriptionHtml;
    }

    public string Ref { get; }
    public int Number { get; }
    public string Title { get; }
    public string DescriptionHtml { get; }
    public async Task GenerateAsync(Stream templateStream, Stream outputStream)
    {
      using var writer = new StreamWriter(outputStream, leaveOpen: true);
      using var reader = new StreamReader(templateStream, leaveOpen: true);
      var line = await reader.ReadLineAsync().ConfigureAwait(false);
      while (line != null)
      {
        var newLine = FindReplaceTokens(line);
        await writer.WriteLineAsync(newLine).ConfigureAwait(false);
        line = await reader.ReadLineAsync().ConfigureAwait(false);
      }

      await writer.FlushAsync().ConfigureAwait(false);
      outputStream.Position = 0;
    }
    private string FindReplaceTokens(string line)
    {
      var sb = new StringBuilder(line);
      var tokens = _tokenReplacers.Keys;
      foreach (var token in tokens)
      {
        var replacer = _tokenReplacers[token];
        sb.Replace(token, replacer(this));
      }
      return sb.ToString();
    }

    private readonly Dictionary<string, Func<Problem, string>> _tokenReplacers
      = new()
      {
        { "##Ref##", details => details.Ref },
        { "##Title##", details => details.Title },
        { "##No##", details => details.Number.ToString() },
        { "##Details##", details => details.DescriptionHtml }
      };
  }
}
