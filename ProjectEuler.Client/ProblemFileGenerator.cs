using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectEuler.Client
{
    public class ProblemFileGenerator
    {
        private readonly Dictionary<string, string> _tokenMap;
        public ProblemFileGenerator(ProblemDetails details)
        {
            _tokenMap = new Dictionary<string, string>
            {
                {"{{ ref }}",   details.Ref},
                {"{{ title }}", details.Title},
                {"{{ # }}",     details.Number.ToString()},
                {"{{ details }}", details.DetailsHtml}
            };
        }

        public async Task GenerateAsync(string templatePath, string outputPath, CancellationToken cancellationToken = default) {
            using var stream = new FileStream(outputPath, FileMode.CreateNew);
            using var writer = new StreamWriter(stream);

            var lines = await File.ReadAllLinesAsync(templatePath, cancellationToken).ConfigureAwait(false);
            foreach(var line in lines) {
                var tempLine = line;
                foreach(var token in _tokenMap) {
                    if (tempLine.Contains(token.Key)) {
                        tempLine = tempLine.Replace(token.Key, token.Value);
                    }
                }
                await writer.WriteLineAsync(tempLine).ConfigureAwait(false);
            }
        }
    }
}
