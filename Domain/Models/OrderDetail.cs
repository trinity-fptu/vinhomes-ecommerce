namespace Domain.Models;

public class OrderDetail
{
    // Properties
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ProductName { get; set; }
    public string Note { get; set; }
    
    // Navigation Properties
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    
    public Order Order { get; set; }
    public Product Product { get; set; }
}

