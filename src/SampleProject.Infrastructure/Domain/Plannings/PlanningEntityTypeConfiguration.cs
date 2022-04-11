using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleProject.Domain.Plannings;
using SampleProject.Infrastructure.Database;

namespace SampleProject.Infrastructure.Domain.Plannings
{
    internal sealed class PlanningEntityTypeConfiguration : IEntityTypeConfiguration<Planning>
    {
        public void Configure(EntityTypeBuilder<Planning> builder)
        {
            builder.ToTable("Plannings", SchemaNames.Orders);
            
            builder.HasKey(b => b.Id);
        }
    }
}