using MediatR;
using Domain;

namespace Application;

internal class CreateSensorDataCommandHandler(ISensorDataRepository sensorRepository) : IRequestHandler<CreateSensorDataCommand, ResponseBase>
{
    public async Task<ResponseBase> Handle(CreateSensorDataCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateRequest(request);
        
        if(validationResult != null)
            return validationResult;
        
        var sensorData = request.SensorData.FromDto();
        await sensorRepository.CreateAsync(sensorData);

        return new ResponseBase{Success = true, Message = "SensorData is created"};
    }

    private ResponseBase? ValidateRequest(CreateSensorDataCommand request)
    {
         if(request.SensorData == null)
            return new ResponseBase{Success = false, Message = "SensorData is null"};

         if(request.SensorData.SensorId <= 0)
            return new ResponseBase{Success = false, Message = "SensorData.SensorId is wrong"};

          if(!(request.SensorData.Timestamp > DateTime.MinValue && request.SensorData.Timestamp < DateTime.MaxValue))
            return new ResponseBase{Success = false, Message = "SensorData.Timestamp is wrong"};

          if(request.SensorData.Value <= 0)
            return new ResponseBase{Success = false, Message = "SensorData.Value is wrong"};
        
        return null;
    }
}