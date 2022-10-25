using ShopMarket.Models.Pagination;
using ShopMarket.Repository.Interface;
using ShopMarket.ViewModels;

namespace ShopMarket.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;

        public ProductRepository(NorthwindContext context, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository) : base(context)
        {
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
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

        public ProductCreateViewModel CreateProduct()
        {
            var customer = new ProductCreateViewModel(/*_context.Products.ToList(), _context.Categories.ToList()*/)
            {
                Categories = _categoryRepository.GetCategories(),
                Suppliers = _supplierRepository.GetSuppliers()
            };
            return customer;
        }
         
    }
}
