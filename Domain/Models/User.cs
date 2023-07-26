using Domain.Enums;

namespace Domain.Models;

public class User
{
    // Properties
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhotoUrl { get; set; }
    public string FirebaseUID { get; set; }
    public UserStatus Status { get; set; }

    // Navigation Properties
    public Guid? StoreId { get; set; }
    public Guid RoleId { get; set; }

    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Order> Orders { get; set; }
    public UserRole Role { get; set; }
    public Store? Store { get; set; }
}