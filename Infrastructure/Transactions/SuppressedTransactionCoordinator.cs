using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Transactions
{
    internal sealed class SuppressedTransactionCoordinator : TransactionCoordinator
    {
        public SuppressedTransactionCoordinator(params IUnitOfWork[] unitOfWorks)
            : base(unitOfWorks)
        {
        }

    }
}
