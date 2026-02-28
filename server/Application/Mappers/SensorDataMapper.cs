using Domain;
namespace Application;

public static class SensorDataMapper
{
    public static SensorDataDto ToDto(this SensorData entity)
    {
        return new SensorDataDto
        {
            Id = entity.Id,
            SensorId = entity.SensorId,
            Timestamp = entity.Timestamp,
            Value = entity.Value
        };
    }

    public static SensorData FromDto(this SensorDataDto dto)
    {
        return new SensorData
        {
            Id = dto.Id,
            SensorId = dto.SensorId,
            Timestamp = dto.Timestamp,
            Value = dto.Value
        };
    }
}