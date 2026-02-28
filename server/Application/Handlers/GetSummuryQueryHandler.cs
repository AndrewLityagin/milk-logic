using MediatR;
using Domain;

namespace Application;

internal class GetSummuryQueryHandler(ISensorDataRepository sensorRepository) : IRequestHandler<GetSummuryQuery, SummuryDto>
{
    public async Task<SummuryDto> Handle(GetSummuryQuery request, CancellationToken cancellationToken)
    {
        var query = sensorRepository.Entities;
        query = query.Where(sd => sd.SensorId == request.SensorId)
                     .Where(sd => sd.Timestamp >= request.Start && sd.Timestamp <= request.End);

        var entities = await sensorRepository.GetListAsync(query);

        if(entities.Count() == 0)
            return  new SummuryDto
            {
                SensorId = request.SensorId,
                Start = request.Start,
                End = request.End,
                Max = 0,
                Min = 0,
                Avg = 0
            };
        
        return new SummuryDto
        {
            SensorId = request.SensorId,
            Start = request.Start,
            End = request.End,
            Max = entities.Max(sd => sd.Value),
            Min = entities.Min(sd => sd.Value),
            Avg = entities.Average(sd => sd.Value)
        };
    }
}