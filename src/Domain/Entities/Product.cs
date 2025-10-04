using Domain.Common;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; } = null!;
        // Add product-specific fields here
    }
}
