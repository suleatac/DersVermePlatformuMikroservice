using Microservice.Order.Application.Contracts.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Persistence.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
           await context.Database.BeginTransactionAsync(cancellationToken);
        }

        public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            return context.Database.CommitTransactionAsync(cancellationToken);
        }

        public Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
