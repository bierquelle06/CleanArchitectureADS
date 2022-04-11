using System;

namespace SampleProject.Domain.Customers
{
    public interface IOrderUniquenessChecker
    {
        bool IsUnique(Guid orderId);
    }
}