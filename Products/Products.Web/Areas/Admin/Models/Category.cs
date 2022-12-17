using System.ComponentModel.DataAnnotations;

namespace Products.Web.Admin.Models
{
    public class Category : BaseEntity
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Product> Products { get; set; }

    }
}
