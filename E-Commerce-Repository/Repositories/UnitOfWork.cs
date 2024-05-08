using E_Commerce_Core.Enities;
using E_Commerce_Core.Interfaces.Repositories;
using E_Commerce_Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        private readonly Hashtable _repositories;
        public UnitOfWork(DataContext context )
        {
            _context = context;
            _repositories = new();
        }
        public async Task<int> CompleteAsync()=>await _context.SaveChangesAsync();
       

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName=typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeName))
            {
                return (_repositories[typeName] as GenericRepository<TEntity, TKey>)!;
            }

            var repo = new GenericRepository<TEntity, TKey>(_context);

            _repositories.Add(typeName,repo) ;
            return repo;

        }
        public async ValueTask DisposeAsync()=> await _context.DisposeAsync();
        

    }
}
