namespace Domain;

public interface ISensorDataRepository 
{
    IQueryable<SensorData> Entities { get; }
    Task<SensorData> CreateAsync(SensorData sensorData);
    Task<IEnumerable<SensorData>> GetAsync(IQueryable<SensorData> query);  
    Task<SensorData> UpdateAsync(SensorData sensorData);    
    Task DeleteAsync(int sensorDataId);  
}