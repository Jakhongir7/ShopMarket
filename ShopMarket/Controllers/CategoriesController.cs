using Microsoft.AspNetCore.Mvc;
using ShopMarket.Repository;
using ShopMarket.Repository.Interface;
using ShopMarket.ViewModels;

namespace ShopMarket.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ICategoryRepository categoryRepository, ILogger<CategoriesController> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Messages of Logs in method Index()");
            CategoryListViewModel categoriesListViewModel = new CategoryListViewModel(_categoryRepository.AllCategories);
            return View(categoriesListViewModel);
        }
    }
}
