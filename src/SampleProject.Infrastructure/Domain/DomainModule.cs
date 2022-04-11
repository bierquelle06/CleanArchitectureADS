﻿using Autofac;
using SampleProject.Application.Customers.DomainServices;
using SampleProject.Domain.Customers;

namespace SampleProject.Infrastructure.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerUniquenessChecker>()
                .As<ICustomerUniquenessChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderUniquenessChecker>()
                .As<IOrderUniquenessChecker>()
                .InstancePerLifetimeScope();
        }
    }
}