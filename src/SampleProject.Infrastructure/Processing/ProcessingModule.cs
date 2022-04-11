using System.Reflection;
using Autofac;
using MediatR;
using SampleProject.Application;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.DomainEvents;
using SampleProject.Application.Configuration.Processing;

using SampleProject.Infrastructure.Logging;
using SampleProject.Infrastructure.Processing.InternalCommands;

namespace SampleProject.Infrastructure.Processing
{
    public class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventsDispatcher>()
                .As<IDomainEventsDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterGenericDecorator(
                typeof(DomainEventsDispatcherNotificationHandlerDecorator<>), 
                typeof(INotificationHandler<>));

            builder.RegisterGenericDecorator(
                typeof(UnitOfWorkCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));

            builder.RegisterGenericDecorator(
                typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandler<,>));

            builder.RegisterType<CommandsDispatcher>()
                .As<ICommandsDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandsScheduler>()
                .As<ICommandsScheduler>()
                .InstancePerLifetimeScope();

            builder.RegisterGenericDecorator(
                typeof(LoggingCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));

            builder.RegisterGenericDecorator(
                typeof(LoggingCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandler<,>));
        }
    }
}