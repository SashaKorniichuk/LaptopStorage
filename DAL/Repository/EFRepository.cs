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

        public async Task Create(TEntity entity)
        {
            _set.Add(entity);
            await _dBContext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _set.AsEnumerable();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
