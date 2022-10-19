using Microsoft.AspNetCore.Mvc;
using ShopMarket.Extensions;
using ShopMarket.Repository;
using ShopMarket.Repository.Interface;
using ShopMarket.ViewModels;

namespace ShopMarket.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index(int page = 1)
        {
            var dataPage = LinqExtensions.GetPaged(_productRepository.AllProducts, page, 10);
            ProductListViewModel piesListViewModel = new ProductListViewModel(_productRepository.AllProducts, dataPage);
            return View(piesListViewModel);
        }
    }
}