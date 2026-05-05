using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    public class GetAndListIntegrationTests : IClassFixture<TestBase>
    {
        private readonly TestBase _factory;

        public GetAndListIntegrationTests(TestBase factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/products/1")]
        [InlineData("/api/customers/1")]
        [InlineData("/api/orders/1")]
        [InlineData("/api/orderitems/1")]
        public async Task Get_ShouldReturnSuccessStatusCode(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData("/api/products?pageNumber=1&pageSize=10")]
        [InlineData("/api/customers?pageNumber=1&pageSize=10")]
        [InlineData("/api/orders?pageNumber=1&pageSize=10")]
        [InlineData("/api/orderitems?pageNumber=1&pageSize=10")]
        public async Task List_ShouldNotReturnNotFound(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.StatusCode.Should().NotBe(HttpStatusCode.NotFound);
        }
    }
}