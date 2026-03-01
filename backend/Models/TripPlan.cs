namespace TravelApp.Models;

public partial class TripPlan
{
    public Guid TripPlanId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<TripPlace> TripPlaces { get; set; } = new List<TripPlace>();

    public virtual User User { get; set; } = null!;
}
