using Domain.Enums;

namespace Domain.Models;

public class Order
{
    // Properties
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderNumber { get; set; }
    public string? Address { get; set; }
    public double Total { get; set; }
    public Guid CustomerId { get; set; }
    public Guid StoreId { get; set; }
    public string? FCMToken { get; set; }

    public OrderStatus Status { get; set; }
    // Navigation Properties
    public User User { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public Store Store { get; set; }

}

