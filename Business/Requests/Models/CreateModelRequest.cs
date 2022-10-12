namespace Business.Requests.Models;

public class CreateModelRequest
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int BrandId { get; set; }
    public int FuelId { get; set; }
    public int TransmissionId { get; set; }
}