namespace Domain.Models;

public class Product
{
    // Properties
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }

    // Navigation Properties
    public Guid CategoryId { get; set; }
    public Guid StoreId { get; set; }
    
    public Category Category { get; set; }
    public Inventory Inventory { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public Store Store { get; set; }
    
}
