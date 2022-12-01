using EntitiesLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interfaces
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        Task<List<T>> GetAll();
        //geriye t dönden getbyıd veobjeden bir id değeri al.find metod kullancaz
        Task<T> Find(object id);
        //crate metodu de T den bi entity al.
        //expression clasından faydalanıp func delegete verdik
        //t tipinden olcak bool tipinden dönecek, bir filter
        Task<T> GetByFilter(Expression<Func<T, bool>> filter, bool asNoTracking=false );
        Task Create(T entity);
        void Update(T entity,T unchanged);
        void Remove(T entity);
        IQueryable<T> GetQueryable();
        //ilgili interface oluştu ve bunların klasını oluşturalım

    }
}
