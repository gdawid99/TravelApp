using System;
using System.Collections.Generic;

namespace TravelApp.Models;

public partial class Place
{
    public Guid PlaceId { get; set; }

    public string Name { get; set; } = null!;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string OsmId { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<TripPlace> TripPlaces { get; set; } = new List<TripPlace>();
}
