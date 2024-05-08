using E_Commerce_Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.Interfaces.Repositories
{
   
        public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
        {
            Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> specification);
        Task<TEntity> GetAsync(TKey Id);

        Task<int> GetProductCountAsync(ISpecification<TEntity> specification);
        Task<TEntity> GetWithSpecAsync(ISpecification<TEntity> specification);
        Task AddAsync(TEntity entity);

            void Delete(TEntity entity);

            void Update(TEntity entity);
        }
    
}
