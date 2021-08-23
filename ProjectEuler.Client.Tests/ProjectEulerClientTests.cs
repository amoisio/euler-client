using Xunit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectEuler.Client.Tests
{
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
