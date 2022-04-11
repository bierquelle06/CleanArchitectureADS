using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Plannings;
using SampleProject.Domain.SharedKernel;
using SampleProject.Infrastructure.Database;

namespace SampleProject.Infrastructure.Domain.Customers
{
    internal sealed class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        internal const string OrdersList = "_orders";
        internal const string OrderPlannings = "_orderPlannings";

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", SchemaNames.Orders);
            
            builder.HasKey(b => b.Id);

            builder.Property("_email").HasColumnName("Email");
            builder.Property("_name").HasColumnName("Name");
            
            builder.OwnsMany<Order>(OrdersList, x =>
            {
                x.WithOwner().HasForeignKey("CustomerId");

                x.ToTable("Orders", SchemaNames.Orders);
                
                x.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
                x.Property<string>("_description").HasColumnName("Description");
                x.Property<DateTime>("_orderDate").HasColumnName("OrderDate");
                x.Property<DateTime?>("_orderChangeDate").HasColumnName("OrderChangeDate");
                x.Property<OrderId>("Id");
                x.HasKey("Id");

                x.Property("_status").HasColumnName("StatusId").HasConversion(new EnumToNumberConverter<OrderStatus, byte>());

                x.OwnsMany<OrderPlanning>(OrderPlannings, y =>
                {
                    y.WithOwner().HasForeignKey("OrderId");

                    y.ToTable("OrderPlannings", SchemaNames.Orders);
                    y.Property<OrderId>("OrderId");
                    y.Property<PlanningId>("PlanningId");
                    
                    y.HasKey("OrderId", "PlanningId");
                });
            });
        }
    }
}