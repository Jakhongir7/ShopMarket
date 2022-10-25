using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMarket.Repository.Interface;

namespace ShopMarket.Repository.Interface
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IEnumerable<Category> AllCategories { get; }
        IEnumerable<SelectListItem> GetCategories();
    }
}
