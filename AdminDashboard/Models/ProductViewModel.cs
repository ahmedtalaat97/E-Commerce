using E_Commerce_Core.Enities;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models
{
    public class ProductViewModel 
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public IFormFile ? Image { get; set; }

        public string? PictureUrl { get; set; }


        [Required(ErrorMessage = "Price is required")]
        [Range(0,100000)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "ProductTypeId is required")]
        public int ProductTypeId { get; set; }

        public ProductType? ProductType { get; set; }

        [Required(ErrorMessage = "ProductBrandId is required")]
        public int ProductBrandId { get; set; }
        public ProductBrand? ProductBrand { get; set; }
     
    }
}
