namespace CoWorkSpace.Domain.DTOs.Requests;

public class UpdateReservationRequestDTO
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ReservationStatus Status { get; set; }
}