using MediatR;

namespace Application;

public class CreateSensorDataCommand(SensorDataDto sensorData) : IRequest<ResponseBase>
{
    public SensorDataDto SensorData { get; set; } = sensorData;
}