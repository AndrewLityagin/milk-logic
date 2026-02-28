namespace Application;

public class SummuryDto
{
    public int SensorId {get; set; }
    public DateTime Start {get; set; }
    public DateTime End {get; set; }
    public float Min {get; set; }
    public float Avg {get; set; }
    public float Max {get; set; }
}