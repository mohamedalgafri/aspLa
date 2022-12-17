using System.ComponentModel.DataAnnotations;

namespace Products.Web.Admin.ViewModel
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
