using Microservice.Order.Application.Contracts.Repositories;
using Microservice.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Order.Persistence.Repositories
{
    public class GenericRepository<TId, Tentity>(AppDbContext context) : IGenericRepository<TId, Tentity> where Tentity : BaseEntity<TId> where TId : struct
    {
        protected readonly AppDbContext _context = context;
        protected readonly DbSet<Tentity> _dbSet = context.Set<Tentity>();
        public void Add(Tentity entity)
        {
            _dbSet.Add(entity);
        }

        public Task<bool> AnyAsync(TId id)
        {
            return _dbSet.AnyAsync(e => e.Id.Equals(id));
        }

        public Task<bool> AnyAsync(Expression<Func<Tentity, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate);
        }

        public void Remove(Tentity entity)
        {
            _dbSet.Remove(entity);
        }

        public Task<List<Tentity>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public Task<List<Tentity>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public ValueTask<Tentity?> GetByIdAsync(TId id)
        {
            return _dbSet.FindAsync(id);
        }

        public void Update(Tentity entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<Tentity> Where(Expression<Func<Tentity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
