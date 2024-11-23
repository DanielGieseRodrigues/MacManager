using MacManager.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace MacManager.Infra.Repositories
{
    //Repositorio base, uso em diversos projetos esse modelo dessa classezinha generica. Faz bem o trabalho de generalizar os metodos mais padroes de CRUD, e os mais especificos ficam na classe que herdará dela.
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
