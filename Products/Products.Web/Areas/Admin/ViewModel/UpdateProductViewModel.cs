using System.ComponentModel.DataAnnotations;

namespace Products.Web.Admin.ViewModel
{
    public class UpdateProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Discrabtion { get; set; }


    }
}
