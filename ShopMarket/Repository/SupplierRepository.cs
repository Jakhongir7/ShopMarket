using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMarket.Repository.Interface;

namespace ShopMarket.Repository
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(NorthwindContext context) : base(context)
        {
        }

        public IEnumerable<Supplier> AllSuppliers => _context.Suppliers.OrderBy(p => p.CompanyName);

        public IEnumerable<SelectListItem> GetSuppliers()
        {
            
                List<SelectListItem> suppliers = _context.Suppliers
                    .OrderBy(n => n.CompanyName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.SupplierId.ToString(),
                            Text = n.CompanyName
                        }).ToList();
                var suppliertip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- select supplier ---"
                };
                suppliers.Insert(0, suppliertip);
                return new SelectList(suppliers, "Value", "Text");
            
        }
    }
}
