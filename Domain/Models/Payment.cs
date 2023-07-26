namespace Domain.Models;

public class Payment
{
    // Properties
    public Guid Id { get; set; }
    public double Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Method { get; set; }
    public string Status { get; set; }
    
    // Navigation Properties
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    
    public Order Order { get; set; }
    public User User { get; set; }
    
}

