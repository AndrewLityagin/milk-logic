using MediatR;

namespace Application;

public class GetSensorDataListQuery(DateTimeOffset start, DateTimeOffset end): IRequest<List<SensorDataDto>>
{
    public DateTimeOffset Start { get; set; } = start;
    public DateTimeOffset End { get; set; } = end;
}
