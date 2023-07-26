namespace Domain.Models;

public class Category
{
    // Properties
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    // Navigation Properties
    public ICollection<Product> Products { get; set; }
}


