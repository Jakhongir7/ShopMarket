using ShopMarket.Models.Pagination;
using ShopMarket.Repository.Interface;

namespace ShopMarket.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext _context;

        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> AllProducts
        {
            get
            {
                return _context.Products.Include(c => c.Category).Include(s => s.Supplier);
            }
        }

        public Product? GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
