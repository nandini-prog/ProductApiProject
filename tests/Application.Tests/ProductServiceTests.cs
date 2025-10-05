using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Application.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IGenericRepository<Product>> _mockRepo;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            // Create a mock repository and inject into ProductService
            _mockRepo = new Mock<IGenericRepository<Product>>();
            _service = new ProductService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnProducts()
        {
            // Arrange
            var fakeProducts = new List<Product>
            {
                new() { Id = 1, ProductName = "Keyboard" },
                new() { Id = 2, ProductName = "Mouse" }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(fakeProducts);

            // Act
            var result = await _service.GetAllProductsAsync();

            // Assert
            result.Should().HaveCount(2);
            result.First().ProductName.Should().Be("Keyboard");
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnCorrectProduct()
        {
            // Arrange
            var product = new Product { Id = 1, ProductName = "Monitor" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _service.GetProductByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.ProductName.Should().Be("Monitor");
        }

        [Fact]
        public async Task CreateProductAsync_ShouldAddAndReturnProduct()
        {
            // Arrange
            var product = new Product { ProductName = "Laptop" };

            // Act
            var result = await _service.CreateProductAsync(product);

            // Assert
            result.ProductName.Should().Be("Laptop");
            _mockRepo.Verify(r => r.AddAsync(product), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldModifyAndReturnUpdatedEntity()
        {
            // Arrange
            var existing = new Product { Id = 1, ProductName = "OldName" };
            var updated = new Product { ProductName = "NewName" };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

            // Act
            var result = await _service.UpdateProductAsync(1, updated);

            // Assert
            result.Should().NotBeNull();
            result!.ProductName.Should().Be("NewName");
            _mockRepo.Verify(r => r.Update(existing), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldReturnTrue_WhenProductExists()
        {
            // Arrange
            var existing = new Product { Id = 1, ProductName = "Tablet" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

            // Act
            var result = await _service.DeleteProductAsync(1);

            // Assert
            result.Should().BeTrue();
            _mockRepo.Verify(r => r.Remove(existing), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
