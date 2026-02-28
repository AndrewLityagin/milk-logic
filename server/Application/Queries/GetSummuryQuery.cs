using MediatR;

namespace Application;

public class GetSummuryQuery(int sensorId, DateTime start, DateTime end): IRequest<SummuryDto>
{
    public int SensorId { get; set; } = sensorId;
    public DateTime Start { get; set; } = start;
    public DateTime End { get; set; } = end;
}