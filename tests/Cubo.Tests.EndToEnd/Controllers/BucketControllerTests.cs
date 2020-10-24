using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using FluentAssertions;
using Xunit;
using Cubo.Api;

namespace Cubo.Tests.EndToEnd.Controllers
{
    public class BucketControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public BucketControllerTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task fetching_bucket_names_should_return_empty_collection()
        {
            var response = await _client.GetAsync("api/buckets");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var buckets = JsonSerializer.Deserialize<IEnumerable<string>>(content);

            buckets.Should().BeEmpty();
        }

        [Fact]
        public async Task creating_new_bucket_should_succeed()
        {
            var name = "test-bucket";

            var response = await _client.PostAsync($"api/buckets/{name}", GetPayload(new { }));
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().BeEquivalentTo($"buckets/{name}");
        }

        [Fact]
        public async Task deleting_bucket_should_succeed()
        {
            var name = "test-bucket";

            var response = await _client.DeleteAsync($"api/buckets/{name}");
            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NoContent);
        }

        private StringContent GetPayload(object data)
        {
            var json = JsonSerializer.Serialize(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}