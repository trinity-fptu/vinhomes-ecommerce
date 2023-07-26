namespace Domain.Models;

public class Review
{
    // Properties
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    
    // Navigation Properties
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    
    public Product Product { get; set; }
    public User User { get; set; }
}

