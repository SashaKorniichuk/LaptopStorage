using DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class EFRepository<TEntity> : IGenericRepository<TEntity> where TEntity:class
    {

        private readonly DbContext _dBContext;
        private readonly DbSet<TEntity> _set;


        public EFRepository(DbContext dbContext)
        {
            _dBContext = dbContext;
            _set = _dBContext.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            _set.Add(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity=_set.Find(id);
            _set.Remove(entity);
            await SaveAsync();


        }
        private async Task SaveAsync()
        {
            await _dBContext.SaveChangesAsync();
        }

        public TEntity Get(int id)
        {
            return _set.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _set.AsEnumerable();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dBContext.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
        }
    }
}
