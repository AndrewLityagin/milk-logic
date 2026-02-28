using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class SensorDataRepository(AppDbContext dbContext) : ISensorDataRepository
{
    private readonly AppDbContext dbContext = dbContext;
    public IQueryable<SensorData> Entities => dbContext.SensorDatas;

    public async Task<SensorData> CreateAsync(SensorData sensorData)
    {
        var result = await dbContext.SensorDatas.AddAsync(sensorData);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public bool Delete(int sensorDataId)
    {
        var entity = dbContext.SensorDatas.FirstOrDefault(sd => sd.Id == sensorDataId);
        if(entity != null){ 
            dbContext.SensorDatas.Remove(entity);
            return true;
        }
        return false;
    }

    public async Task<List<SensorData>> GetListAsync(IQueryable<SensorData> query)
    {
        return await query.ToListAsync();
    }

    public async Task UpdateAsync(SensorData sensorData)
    {
        var entity = dbContext.SensorDatas.FirstOrDefault(sd => sd.Id == sensorData.Id);
        if(entity != null){ 
            entity.SensorId = sensorData.SensorId;
            entity.Timestamp = sensorData.Timestamp;
            entity.Value = sensorData.Value;
            await dbContext.SaveChangesAsync();
        }
    }
}