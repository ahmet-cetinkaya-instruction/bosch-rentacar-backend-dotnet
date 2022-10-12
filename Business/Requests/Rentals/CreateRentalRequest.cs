namespace Business.Requests.Rentals;

public class CreateRentalRequest
{
    public int CarId { get; set; }
    public int ModelId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}