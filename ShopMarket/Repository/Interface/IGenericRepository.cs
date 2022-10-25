using Microsoft.AspNetCore.Mvc;

namespace ShopMarket.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Add(T obj);
        void Edit(T obj);
        void Delete(T obj);
        void SaveChanges();
    }
}
