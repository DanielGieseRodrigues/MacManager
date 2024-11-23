using MacManager.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace MacManager.Infra.Repositories
{
    public class RepositoryBase<TEntity> where TEntity : class
    {
        protected readonly MacManagerContext _context;

        public RepositoryBase(MacManagerContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> ObterTodosAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task RemoverAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
