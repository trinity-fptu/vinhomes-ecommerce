using Domain.Enums;

namespace Domain.Models;

public class Store
{
    // Properties
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string ImageUrl { get; set; }
    public StoreStatus Status { get; set; }
    
    // Navigation Properties
    public Guid StoreOwnerId { get; set; }
    public User StoreOwner { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<Order> Orders { get; set; }
}