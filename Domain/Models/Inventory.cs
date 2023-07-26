namespace Domain.Models;

public class Inventory
{
    // Properties
    public Guid Id { get; set; }
    public int Quantity { get; set; }  // current quantity of product in stock
    public DateTime LastUpdated { get; set; } // last time the quantity was updated
    
    // Navigation Properties
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}

