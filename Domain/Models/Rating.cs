namespace Domain.Models;

public class Rating
{
    // Properties
    public Guid Id { get; set; }
    public double Value { get; set; }
    
    // Navigation Properties
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    
    public Product Product { get; set; }
    public User User { get; set; }
}

