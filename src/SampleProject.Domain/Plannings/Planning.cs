using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Plannings
{
    public class Planning : Entity, IAggregateRoot
    {
        public PlanningId Id { get; private set; }

        public string Source { get; private set; }

        public string Target { get; private set; }

        private Planning()
        {

        }

        private Planning(string target, string source)
        {
            this.Id = new PlanningId(Guid.NewGuid());

            Source = source;
            Target = target;

            this.AddDomainEvent(new PlanningRegisteredEvent(this.Id));
        }

        public static Planning CreateRegistered(
            string target,
            string source)
        {
            return new Planning(target, source);
        }
    }
}