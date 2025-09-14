namespace CoWorkSpace.Domain.Entities;

public class SpaceAmenity
{
    public int Id { get; set; }
    public int SpaceId { get; set; }
    public Space Space { get; set; } = null!;
    public int AmenityId { get; set; }
    public Amenity Amenity { get; set; } = null!;
}