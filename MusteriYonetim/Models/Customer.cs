namespace MusteriYonetim.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int Balance { get; set; }
}
