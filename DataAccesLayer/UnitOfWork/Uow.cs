using DataAccesLayer.Context;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Repositories;
using EntitiesLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly WorkContext _context;

        public Uow(WorkContext context)
        {
            _context = context;
        }
        //base entity ortak özellikler id
        public IGenericRepository<T> GetRepository<T>() where T :BaseEntity
        {
            return new GenericRepository<T>(_context);
        }

        public async Task SaveChanges()
        {
           await _context.SaveChangesAsync();
        }
    }
}
