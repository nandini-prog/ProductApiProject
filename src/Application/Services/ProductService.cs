using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    /// <summary>
    /// Service layer class that handles business logic for Product-related operations.
    /// Acts as a bridge between the API layer (Controllers) and the data access layer (Repositories).
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;

        /// <summary>
        /// Constructor that injects the generic repository for Product entity.
        /// </summary>
        /// <param name="repository">Generic repository instance used for database operations.</param>
        public ProductService(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>A list of all Product entities.</returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Retrieves a single product by its ID.
        /// </summary>
        /// <param name="id">Unique identifier of the product.</param>
        /// <returns>The Product entity if found, otherwise null.</returns>
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Creates a new product and saves it to the database.
        /// </summary>
        /// <param name="product">Product entity containing the new product details.</param>
        /// <returns>The newly created Product entity.</returns>
        public async Task<Product> CreateProductAsync(Product product)
        {
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Updates an existing product with new values.
        /// </summary>
        /// <param name="id">Unique identifier of the product to update.</param>
        /// <param name="updatedProduct">Product entity containing updated information.</param>
        /// <returns>The updated Product entity, or null if not found.</returns>
        public async Task<Product?> UpdateProductAsync(int id, Product updatedProduct)
        {
            // Retrieve the existing record from the database
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            // Update fields (extend this as more fields are added to Product)
            existing.ProductName = updatedProduct.ProductName;

            _repository.Update(existing);
            await _repository.SaveChangesAsync();

            return existing;
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">Unique identifier of the product to delete.</param>
        /// <returns>True if the product was deleted, false if not found.</returns>
        public async Task<bool> DeleteProductAsync(int id)
        {
            // Find the product first
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            _repository.Remove(existing);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
