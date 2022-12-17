using System.ComponentModel.DataAnnotations;

namespace Products.Web.Admin.ViewModel
{
    public class UpdateCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
