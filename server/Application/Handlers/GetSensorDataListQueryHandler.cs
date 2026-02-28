using MediatR;
using Domain;

namespace Application;

internal class GetSensorDataListQueryHandler(ISensorDataRepository sensorRepository) : IRequestHandler<GetSensorDataListQuery, List<SensorDataDto>>
{
    public async Task<List<SensorDataDto>> Handle(GetSensorDataListQuery request, CancellationToken cancellationToken)
    {
        var query = sensorRepository.Entities;
        query = query.Where(sd => sd.SensorId == request.SensorId)
                     .Where(sd => sd.Timestamp >= request.Start && sd.Timestamp <= request.End);
        
        var entities = await sensorRepository.GetListAsync(query);

        if(entities.Count() == 0)
            return new List<SensorDataDto>();
        
        return entities.Select(e => e.ToDto()).ToList();
    }
}