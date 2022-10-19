using Microsoft.AspNetCore.Mvc;
using ShopMarket.Models.Pagination;

namespace ShopMarket.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Pagination", result));
        }
    }
}
