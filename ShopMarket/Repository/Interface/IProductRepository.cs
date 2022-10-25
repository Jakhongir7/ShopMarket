using ShopMarket.ViewModels;

namespace ShopMarket.Repository.Interface
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> AllProducts { get; }
        Product? GetProductById(int productId);
        ProductCreateViewModel CreateProduct();
        //ProductCreateViewModel EditProduct(int? id);
    }
}
