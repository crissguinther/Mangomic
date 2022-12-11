using System.Linq.Expressions;

namespace Mangomic.DAL {
    public interface IRepository<TEntity> where TEntity : class {
        void Add(TEntity entity);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();
        void Remove(TEntity entity);
    }
}