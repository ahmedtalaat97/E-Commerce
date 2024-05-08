using E_Commerce_Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        IGenericRepository<TEntity,TKey> Repository<TEntity,TKey>() where TEntity:BaseEntity<TKey>;

        Task<int> CompleteAsync();

    }
}
