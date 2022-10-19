namespace ShopMarket.Repository.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        Product? GetProductById(int productId);
    }
}
