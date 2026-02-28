namespace Domain;

public interface ISensorDataRepository 
{
    IQueryable<SensorData> Entities { get; }
    Task<SensorData> CreateAsync(SensorData sensorData);
    Task<List<SensorData>> GetListAsync(IQueryable<SensorData> query);  
    Task UpdateAsync(SensorData sensorData);    
    bool Delete(int sensorDataId);  
}