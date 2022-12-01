using DataAccesLayer.Context;
using DataAccesLayer.Interfaces;
using EntitiesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T :BaseEntity
    {
        private readonly WorkContext _context;

        public GenericRepository(WorkContext context)
        {
            _context = context;
        }

        //UOW ile DI ile aldığın örneği  buranın constructorına vercez
        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByFilter(Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            return asNoTracking ? await _context.Set<T>().SingleOrDefaultAsync(filter) : await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<T> Find(object id)
        {
            return await _context.Set<T>().FindAsync(id); 
        }

        public IQueryable<T> GetQueryable()
        {
           return _context.Set<T>().AsQueryable();
        }
        //silmeyi repo üzerinden yapıyo service değil
        public void Remove(T entity)
        {
           
            _context.Set<T>().Remove(entity);
        }
        //update ve delete asyn değil
        public void  Update(T entity,T Unchanged)
        {
            //properyt bazında setleme olcak, yani sadece değiştirmiş olduğumuz özellik değişcek diğeri kalcak.
            //var updatedentity = _context.Set<T>().Find(entity.Id);
            _context.Entry(Unchanged).CurrentValues.SetValues(entity);
        }
    }
}
