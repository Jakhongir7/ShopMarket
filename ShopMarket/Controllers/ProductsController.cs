using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMarket.Extensions;
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

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
        }
        public IActionResult Index(int page = 1)
        {
            var dataPage = LinqExtensions.GetPaged(_productRepository.AllProducts, page, 10);
            ProductListViewModel piesListViewModel = new ProductListViewModel(_productRepository.AllProducts, dataPage);
            return View(piesListViewModel);
        }

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
                    // Настройка конфигурации AutoMapper
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductCreateViewModel, Product>()
                        .ForMember("CategoryId", opt => opt.MapFrom(s => s.SelectedCategoryName))
                        .ForMember("SupplierId", opt => opt.MapFrom(c => c.SelectedSupplierName)));
                    var mapper = new Mapper(config);
                    // Выполняем сопоставление
                    Product product = mapper.Map<ProductCreateViewModel, Product>(productCreateViewModel);

                    //var CategoryId = productCreateViewModel.SelectedCategoryName != null ?
                    //    int.Parse(productCreateViewModel.SelectedCategoryName) : default;
                    //Product product = new Product()
                    //{
                    //    Category = _categoryRepository.GetById(CategoryId)
                    //};
                    //product.CategoryId = CategoryId;
                    //product.ProductName = productCreateViewModel.ProductName;
                    //product.QuantityPerUnit = productCreateViewModel.QuantityPerUnit;
                    //product.UnitPrice = productCreateViewModel.UnitPrice;
                    //product.UnitsInStock = productCreateViewModel.UnitsInStock;
                    //product.UnitsOnOrder = productCreateViewModel.UnitsOnOrder;
                    //product.ReorderLevel = productCreateViewModel.ReorderLevel;
                    //product.Discontinued = productCreateViewModel.Discontinued;
                    //var mapper = InitializeAutomapper();
                    //var productModel = mapper.Map<ProductCreateViewModel, Product>(productCreateViewModel);
                    _productRepository.Add(product);
                    _productRepository.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(productCreateViewModel);
        }

        //private Mapper InitializeAutomapper()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {
        //        cfg.CreateMap<Product, ProductCreateViewModel>()
        //       .ForMember(dest => dest.SelectedCategoryName, act => act.MapFrom(src => src.Category.CategoryName));
        //    });

        //    var mapper = new Mapper(config);
        //    return mapper;
        //}

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetById(id);

            // Настройка конфигурации AutoMapper
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductCreateViewModel>()
                .ForMember("SelectedCategoryName", opt => opt.MapFrom(s => s.CategoryId))
                .ForMember("SelectedSupplierName", opt => opt.MapFrom(c => c.SupplierId))
                );
            var mapper = new Mapper(config);
            // Выполняем сопоставление
            ProductCreateViewModel productCreateViewModel = mapper.Map<Product, ProductCreateViewModel>(product);


            if (productCreateViewModel == null)
            {
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
                return NotFound();
            }
            
            if (productCreateViewModel != null)
            {
                try
                {
                    // Настройка конфигурации AutoMapper
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductCreateViewModel, Product>()
                        .ForMember("CategoryId", opt => opt.MapFrom(s => s.SelectedCategoryName))
                        .ForMember("SupplierId", opt => opt.MapFrom(c => c.SelectedSupplierName))
                        );
                    var mapper = new Mapper(config);
                    // Выполняем сопоставление
                     Product product = mapper.Map<ProductCreateViewModel, Product>(productCreateViewModel);
                    _productRepository.Edit(product);
                    _productRepository.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(productCreateViewModel);
        }

        public IActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.AllProducts.FirstOrDefault(s => s.ProductId == id);

            if (product == null)
            {
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

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _productRepository.Delete(product);
                _productRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

    }
}