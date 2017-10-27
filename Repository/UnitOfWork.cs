using EMS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.Repository
{
    public class UnitOfWork: IDisposable
    {
        private readonly AdventureWorks2014Context _context;
       // private bool disposed;
        private Dictionary<string, object> repositories;
        private bool disposed;

        public UnitOfWork(AdventureWorks2014Context context)
        {
            _context = context;
        }

        public AdventureWorks2014Context GetContext()
        {
            return _context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public GenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(TEntity).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                repositories.Add(type, repositoryInstance);
            }
            return (GenericRepository<TEntity>)repositories[type];
        }
    }
}
