using System.ComponentModel.DataAnnotations;

namespace Products.Web.Admin.ViewModel
{
    public class CreateProductViewModel
    {

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public IFormFile ImageUrl { get; set; }
    }
}
