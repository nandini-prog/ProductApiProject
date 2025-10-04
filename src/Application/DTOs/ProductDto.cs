namespace Application.DTOs
{
    // Public representation of Product returned to API clients
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
    }
}
