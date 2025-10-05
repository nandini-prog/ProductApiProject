using System.Net;
using System.Net.Http.Json;
using API.Tests.Factory;
using Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace API.Tests
{
    public class ProductApiIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductApiIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_ShouldReturnSuccessAndEmptyListInitially()
        {
            var response = await _client.GetAsync("/api/products");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            products.Should().BeEmpty();
        }

        [Fact]
        public async Task PostProduct_ShouldCreateAndReturnProduct()
        {
            var newProduct = new Product { ProductName = "Keyboard", CreatedBy = "TestUser" };

            var response = await _client.PostAsJsonAsync("/api/products", newProduct);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var created = await response.Content.ReadFromJsonAsync<Product>();
            created!.ProductName.Should().Be("Keyboard");
        }

        [Fact]
        public async Task GetProductById_ShouldReturnProduct_WhenExists()
        {
            // Create product first
            var created = await _client.PostAsJsonAsync("/api/products", new Product { ProductName = "Mouse", CreatedBy = "Test" });
            var saved = await created.Content.ReadFromJsonAsync<Product>();

            var response = await _client.GetAsync($"/api/products/{saved!.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
