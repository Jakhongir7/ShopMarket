using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ShopMarket.ViewModels
{
    public class ProductCreateViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "ProductName Name")]
        [StringLength(100)]
        public string? ProductName { get; set; }

        [Required]
        [Display(Name = "Quantity Per Unit")]
        [StringLength(25)]
        public string? QuantityPerUnit { get; set; }

        [Required]
        [Display(Name = "Unit Price")]
        [Range(0, int.MaxValue)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public decimal? UnitPrice { get; set; }

        [Required]
        [Display(Name = "Unit Price")]
        [Range(0, int.MaxValue)]
        public short? UnitsInStock { get; set; }

        [Required]
        [Display(Name = "Units On Order")]
        [Range(0, int.MaxValue)]
        public short? UnitsOnOrder { get; set; }

        [Required]
        [Display(Name = "Reorder Level")]
        [Range(0, int.MaxValue)]
        public short? ReorderLevel { get; set; }

        [Required]
        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string? SelectedCategoryName { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public string? SelectedSupplierName { get; set; }
        public IEnumerable<SelectListItem>? Suppliers { get; set; }

        //public IEnumerable<Product>? Products { get; }

        //public ProductCreateViewModel(IEnumerable<Product> products, IEnumerable<SelectListItem> categories)
        //{
        //    Products = products;
        //    Categories = categories;
        //}
    }
}
