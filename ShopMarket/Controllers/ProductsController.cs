using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMarket.Extensions;
using ShopMarket.AutoMapper;
using ShopMarket.Logs;
using ShopMarket.Repository;
using ShopMarket.Repository.Interface;
using ShopMarket.ViewModels;
using System.Linq;

namespace ShopMarket.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _logger = logger;
        }
        public IActionResult Index(int page = 1)
        {
            _logger.LogInformation("Messages of Logs in method Index()");
            var dataPage = LinqExtensions.GetPaged(_productRepository.AllProducts, page, 10);
            ProductListViewModel piesListViewModel = new ProductListViewModel(_productRepository.AllProducts, dataPage);
            return View(piesListViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var productCreate = _productRepository.CreateProduct();
            return View(productCreate);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel productCreateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = AutoMapper.AutoMapper.ProductProfile(productCreateViewModel);

                    _productRepository.Add(product);
                    _productRepository.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(MyLogEvents.UpdateItem, ex, "Post Create cannot create");
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(productCreateViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning(MyLogEvents.UpdateItem, "Get Edit({id}) NOT FOUND", id);
                return NotFound();
            }

            Product product = _productRepository.GetById(id);

            // Настройка конфигурации AutoMapper
            ProductCreateViewModel productCreateViewModel = AutoMapper.AutoMapper.ProductCreateViewModelProfile(product);

            if (productCreateViewModel == null)
            {
                _logger.LogWarning(MyLogEvents.UpdateItem, "Get Edit({productCreateViewModel}) NOT FOUND", productCreateViewModel);
                return NotFound();
            }
            productCreateViewModel.Categories = _categoryRepository.GetCategories();
            productCreateViewModel.Suppliers = _supplierRepository.GetSuppliers();

            return View(productCreateViewModel);
        }

        [HttpPost]
        public IActionResult Edit(int? id, ProductCreateViewModel productCreateViewModel)
        {
            if (id == null)
            {
                _logger.LogWarning(MyLogEvents.UpdateItem, "Post Edit({id}) NOT FOUND", id);
                return NotFound();
            }
            
            if (productCreateViewModel != null)
            {
                try
                {
                    Product product = AutoMapper.AutoMapper.ProductProfile(productCreateViewModel);

                    _productRepository.Edit(product);
                    _productRepository.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex )
                {
                    _logger.LogWarning(MyLogEvents.UpdateItem, ex, "Post Edit({id}) cannot edit", id);
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(productCreateViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                _logger.LogWarning(MyLogEvents.DeleteItem, "Get Delete({id}) NOT FOUND", id);
                return NotFound();
            }
            Product product = _productRepository.GetById(id);
            //var product = _productRepository.AllProducts.FirstOrDefault(s => s.ProductId == id);

            if (product == null)
            {
                _logger.LogWarning(MyLogEvents.DeleteItem, "Get Delete({product}) NOT FOUND", product);
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Product product = _productRepository.GetById(id);
            if (product == null)
            {
                _logger.LogWarning(MyLogEvents.DeleteItem, "Post Delete({product}) NOT FOUND", product);
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _productRepository.Delete(product);
                _productRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(MyLogEvents.DeleteItem, ex, "Post Delete({id}) cannot delete", id);
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}