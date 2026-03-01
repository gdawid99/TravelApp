namespace TravelApp.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime SignUpDate { get; set; }

    public DateTime LoginDate { get; set; }

    public int Status { get; set; }

    public int Role { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenCreatedAt { get; set; }

    public DateTime? RefreshTokenExpiresAt { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<TripPlan> TripPlans { get; set; } = new List<TripPlan>();
}
