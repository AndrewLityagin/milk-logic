using MediatR;

namespace Application;

public class GetSensorDataListQuery(DateTime start, DateTime end): IRequest<List<SensorDataDto>>
{
    public DateTime Start { get; set; } = start;
    public DateTime End { get; set; } = end;
}
