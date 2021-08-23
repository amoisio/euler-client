using System;
using System.Threading.Tasks;
using System.Net.Http;
using ProjectEuler.Client;
using System.Collections.Generic;

namespace ProjectEuler.Client.Cli
{
    class Program
    {
        static readonly HttpClient _httpClient = new HttpClient();
        static async Task<int> Main(string[] args)
        {
            var api = new OnlineAPI(_httpClient);
            int problemNumber = ParseProblemNumber(args);
            var details = await api.ProblemDetailsFor(problemNumber);

            System.Console.WriteLine(details.Title);
            System.Console.WriteLine(details.DetailsHtml);

            var gen = new ProblemFileGenerator(details);
            foreach (var (templatePath, outputPath) in ParseTemplatePaths(args)) 
            {
                await gen.GenerateAsync(templatePath, outputPath);
            }
            return 1;
        }

        private static int ParseProblemNumber(string[] args) => Int32.Parse(args[0]);

        private static IEnumerable<(string, string)> ParseTemplatePaths(string[] args) 
        {
            int len = args.Length;
            for(int i = 1; i < len; i += 2) 
            {
                string templatePath = args[i];
                string outputPath = (args.Length > i + 1)
                    ? args[i + 1]
                    : ".";
                yield return (templatePath, outputPath);
            }
            
        }
    }
}
