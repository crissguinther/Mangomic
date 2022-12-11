using Mangomic.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mangomic.DAL {
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class {
        private readonly MangomicContext _context;

        public Repository(MangomicContext context) {
            _context = context;
        }

        public void Add(TEntity entity) {
            _context.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) {
            return _context.Set<TEntity>().Where(predicate);
        }

        public async Task<TEntity> Get(int id) {
            var results = await _context.Set<TEntity>().FindAsync(id);
            return results;
        }

        public async Task<IEnumerable<TEntity>> GetAll() {
            var results = await _context.Set<TEntity>().ToListAsync();
            return results;
        }

        public void Remove(TEntity entity) {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
