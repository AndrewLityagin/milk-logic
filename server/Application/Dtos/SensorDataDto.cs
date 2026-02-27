namespace Application;

public class SensorDataDto
{
    public int SensorId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public float Value { get; set; }
}