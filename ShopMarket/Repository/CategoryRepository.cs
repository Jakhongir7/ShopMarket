using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMarket.Repository.Interface;

namespace ShopMarket.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(NorthwindContext context) : base(context)
        {
        }

        public IEnumerable<Category> AllCategories => _context.Categories.OrderBy(p => p.CategoryName);

        public IEnumerable<SelectListItem> GetCategories()
        {
            
                List<SelectListItem> categories = _context.Categories
                    .OrderBy(n => n.CategoryName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.CategoryId.ToString(),
                            Text = n.CategoryName
                        }).ToList();
                var categorytip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select category ---"
                };
                categories.Insert(0, categorytip);
                return new SelectList(categories, "Value", "Text");
            
        }

    }
}
