namespace TravelApp.Models;

public partial class TripPlace
{
    public Guid TripPlaceId { get; set; }

    public Guid TripPlanId { get; set; }

    public Guid PlaceId { get; set; }

    public DateTime VisitDate { get; set; }

    public string? Note { get; set; }

    public virtual Place Place { get; set; } = null!;

    public virtual TripPlan TripPlan { get; set; } = null!;
}
