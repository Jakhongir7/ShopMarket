using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopMarket.Repository.Interface
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        IEnumerable<Supplier> AllSuppliers { get; }
        IEnumerable<SelectListItem> GetSuppliers();
    }
}
