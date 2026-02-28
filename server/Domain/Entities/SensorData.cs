namespace Domain;

public class SensorData
{
    public required int Id { get; set; }
    public required int SensorId { get; set; }
    public required DateTime Timestamp { get; set; }
    public required float Value { get; set; }
}