using FaceID.Core.Entities;
using FaceID.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceID.Data.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly FaceIdContext dbContext;

        public GenericRepository(FaceIdContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> IfExistsAsync<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return await ApplySpecification(spec).AnyAsync();
        }
        public async Task<int> CountAsync<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return await ApplySpecification(spec).CountAsync<T>();
        }

        private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T : BaseEntity
        {
            return SpecificationEvaluator<T>.GetQuery(dbContext.Set<T>().AsQueryable(), spec);
        }

        public IQueryable<T> GetAll<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return ApplySpecification(spec).AsNoTracking();
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync<T>() where T : BaseEntity
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return await GetAll(spec).ToListAsync();
        }

        public async Task<T> GetAsync<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            await dbContext.Set<T>().AddAsync(entity);
            dbContext.Commit();
            return entity;
        }

        public T UpdateAsync<T>(T entity) where T : BaseEntity
        {
            foreach (var entry in dbContext.ChangeTracker.Entries())
            {
                if (entry.Entity != entity)
                {
                    entry.State = EntityState.Unchanged;
                }
            }

            dbContext.Commit();

            return entity;
        }

        public async Task<T> DeleteAsync<T>(T entity) where T : BaseEntity
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
