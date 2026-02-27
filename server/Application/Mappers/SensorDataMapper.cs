namespace Application;

public static class SensorDataMapper
{
    public static SensorDataDto ToDto(this SensorData entity)
    {
        return new SensorDataDto
        {
            SensorId = entity.SensorId,
            Timestamp = entity.Timestamp,
            Value = entity.Value
        };
    }

    public static SensorDataDto ToDto(this SensorData entity)
    {
        return new SensorDataDto
        {
            SensorId = entity.SensorId,
            Timestamp = entity.Timestamp,
            Value = entity.Value
        };
    }
}