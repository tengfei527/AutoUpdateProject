using System;

namespace Infrastructure.Transactions
{
    public interface ITransactionCoordinator : IUnitOfWork, IDisposable
    {
    }
}
