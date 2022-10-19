using Microsoft.AspNetCore.Mvc;
using ShopMarket.Repository;
using ShopMarket.Repository.Interface;
using ShopMarket.ViewModels;

namespace ShopMarket.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            CategoryListViewModel categoriesListViewModel = new CategoryListViewModel(_categoryRepository.AllCategories);
            return View(categoriesListViewModel);
        }
    }
}
