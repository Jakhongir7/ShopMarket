using ShopMarket.Repository.Interface;

namespace ShopMarket.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public NorthwindContext _context;
        public DbSet<T> table;
        
        public GenericRepository(NorthwindContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Add(T obj)
        {
            table.Add(obj);
        }
        public void Edit(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(T obj)
        {
            table.Remove(obj);
        }
        //public T Find(int Key)
        //{
        //    var dbResult = table.Find(Key);
        //    return dbResult;
        //}
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
