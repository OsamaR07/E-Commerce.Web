using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // Get Type Name
            var typeName = typeof(TEntity).Name;

            // Dic<String,Object> ===> string key [Name Of Type] -- Object From GenericRepository.
            if (_repositories.TryGetValue(typeName, out var repository))
                return (IGenericRepository<TEntity, TKey>)repository;

            // If not found, create a new repository and add it to the dictionary
            var newRepository = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[typeName] = newRepository;
            return newRepository;
        }

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}
