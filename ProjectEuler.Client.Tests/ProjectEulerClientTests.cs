using Xunit;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectEuler.Client.Tests
{
    public class ProjectEulerClientTests
    {
        [Fact]
        public async Task GetDetailsFor_gets_problem_details_from_the_project_euler_site()
        {
            using var httpClient = new HttpClient();
            var provider = new OnlineAPI(httpClient);

            var details = await provider.ProblemDetailsFor(1);

            Assert.Equal(1, details.Number);
            Assert.Equal("Multiples of 3 or 5", details.Title);
        }
    }
}
