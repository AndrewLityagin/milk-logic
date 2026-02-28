using MediatR;

namespace Application;

public class GetSummuryQuery(DateTime start, DateTime end): IRequest<SummuryDto>
{
    public DateTime Start { get; set; } = start;
    public DateTime End { get; set; } = end;
}
