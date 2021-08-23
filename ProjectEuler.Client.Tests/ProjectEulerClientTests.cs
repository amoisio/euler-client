using Xunit;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace ProjectEuler.Client.Tests
{
    public class Problem_Generator : IDisposable 
    {
        private readonly HttpClient _client;
        private readonly OnlineAPI _api;
        private bool disposedValue;

        public Problem_Generator()
        {
            _client = new HttpClient();
            _api = new OnlineAPI(_client);
        }

        [Fact]
        public async Task Should_generate_the_output_file()
        {
            var details = await _api.ProblemDetailsFor(1);
            var generator = new ProblemFileGenerator(details);
            
            var outputPath = $"./{Guid.NewGuid()}Output.txt";
            await generator.GenerateAsync("./Template.txt", outputPath);

            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public async Task Should_generate_a_file_which_contains_templated_information()
        {
            var details = await _api.ProblemDetailsFor(1);
            var generator = new ProblemFileGenerator(details);

            var outputPath = $"./{Guid.NewGuid()}Output.txt";
            await generator.GenerateAsync("./Template.txt", outputPath);

            var contents = await File.ReadAllTextAsync(outputPath);

            Assert.Contains(details.Title, contents);
            Assert.Contains(details.Ref, contents);
            Assert.Contains(details.Number.ToString(), contents);
            Assert.Contains(details.DetailsHtml, contents);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _client.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

    public class OnlineAPI_ProblemDetailsFor : IDisposable  
    {
        private readonly HttpClient _client;
        private readonly OnlineAPI _api;
        private bool disposedValue;

        public OnlineAPI_ProblemDetailsFor()
        {
            _client = new HttpClient();
            _api = new OnlineAPI(_client);
        }

        [Fact]
        public async Task Should_contain_the_problem_number()
        {
            var details = await _api.ProblemDetailsFor(1);

            Assert.Equal(1, details.Number);
        }

        [Fact]
        public async Task Should_contain_the_problem_title()
        {
            var details = await _api.ProblemDetailsFor(1);

            Assert.Equal("Multiples of 3 or 5", details.Title);
        }

        [Fact]
        public async Task Should_contain_the_problem_description()
        {
            var details = await _api.ProblemDetailsFor(1);

            Assert.Contains("Find the sum of all the multiples of 3 or 5 below 1000", details.DetailsHtml);
        }

        [Fact]
        public async Task Should_contain_a_reference_to_the_source_site()
        {
            var details = await _api.ProblemDetailsFor(1);

            Assert.Contains("https://projecteuler.net/problem=1", details.Ref);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _client.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
