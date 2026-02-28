using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure;

public class SensorDataEntityConfiguration : IEntityTypeConfiguration<SensorData>
{
    public void Configure(EntityTypeBuilder<SensorData> builder)
    {
      builder.ToTable("sensordata");

      builder
        .Property(e => e.Id)
        .HasColumnName("id");
        
    builder
        .Property(e => e.SensorId)
        .HasColumnName("sensorid");

    builder
        .Property(e => e.Timestamp)
        .HasColumnName("timestamp");

    builder
        .Property(e => e.Value)
        .HasColumnName("value");
    }
}