using MediatR;
using Domain;

namespace Application;

internal class GetSummuryQueryHandler(ISensorDataRepository sensorRepository) : IRequestHandler<GetSummuryQuery, SummuryDto>
{
    public async Task<SummuryDto> Handle(GetSummuryQuery request, CancellationToken cancellationToken)
    {
        var query = sensorRepository.Entities;
        query = query.Where(sd => sd.Timestamp >= request.Start && sd.Timestamp <= request.End);

        var entities = await sensorRepository.GetListAsync(query);
        
        return new SummuryDto
        {
            Start = request.Start,
            End = request.End,
            Max = entities.Max(sd => sd.Value),
            Min = entities.Min(sd => sd.Value),
            Avg = entities.Average(sd => sd.Value)
        };
    }
}