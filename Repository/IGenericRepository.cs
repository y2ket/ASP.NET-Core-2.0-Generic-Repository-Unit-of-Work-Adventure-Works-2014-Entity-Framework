using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        void Create(string query, string _connection);
        TEntity FindById(int businessEntityID);     
        IEnumerable<TEntity> Read();
        IEnumerable<TEntity> Read(string query);
        IEnumerable<TEntity> Read(Func<TEntity, bool> predicate);
        void Update(TEntity entity);
        void Update(string query, string _connection);
        void Delete(TEntity entity);
        void Save();
    }
}
