namespace TravelApp.Models;

public partial class Photo
{
    public Guid PhotoId { get; set; }

    public string Name { get; set; } = null!;

    public string FileUrl { get; set; } = null!;

    public DateTime UploadedAt { get; set; }

    public Guid PlaceId { get; set; }

    public Guid UserId { get; set; }

    public virtual Place Place { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
