using MediatR;

namespace Application;

internal class GetSensorDataListQueryHandler(ISensorDataRepository sensorRepository): IRequestHandler<GetSensorDataListQuery, List<SensorDataDto>>
{
    public async Task<List<SensorDataDto>> Handle(GetSensorDataListQuery request, CancellationToken cancellationToken)
    {
        var query = sensorRepository.Entities;
        query = query.Where(sd => sd.Timestamp >= request.Start && sd.Timestamp <= request.End);
        var entities = await sensorRepository.GetAsync(query);
        return entities.Select(e => e.ToDto()).ToList();
    }
}