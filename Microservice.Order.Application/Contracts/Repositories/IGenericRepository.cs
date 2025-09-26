using Microservice.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Application.Contracts.Repositories
{
    public interface IGenericRepository<TId,Tentity> where Tentity : BaseEntity<TId> where TId : struct
    {

        public Task<bool> AnyAsync(TId id);
        public Task<bool> AnyAsync(Expression<Func<Tentity,bool>> predicate);
        Task<List<Tentity>> GetAllAsync();
        Task<List<Tentity>> GetAllPagedAsync(int pageNumber, int pageSize);
        ValueTask<Tentity?> GetByIdAsync(TId id);
        IQueryable<Tentity> Where(Expression<Func<Tentity, bool>> predicate);
        void Add(Tentity entity);
        void Update(Tentity entity);
        void Remove(Tentity entity);
    }
}
