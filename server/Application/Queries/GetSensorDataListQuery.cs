using MediatR;

namespace Application;

public class GetSensorDataListQuery(int sensorId, DateTime start, DateTime end): IRequest<List<SensorDataDto>>
{
    public int SensorId { get; set; } = sensorId;
    public DateTime Start { get; set; } = start;
    public DateTime End { get; set; } = end;
}