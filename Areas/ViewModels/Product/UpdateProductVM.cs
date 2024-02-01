using Agency.Models;
using System.ComponentModel.DataAnnotations;

namespace Agency.Areas.ViewModels.Product
{
    public class UpdateProductVM
    {
        [Required(ErrorMessage = "Xana bos ola bilmez")]
        [MaxLength(50, ErrorMessage = "maximum 50 element istifade oluna biler")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Xana bos ola bilmez")]
        [MaxLength(300, ErrorMessage = "maximum 300 element istifade oluna biler")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Xana bos ola bilmez")]
        [MaxLength(50, ErrorMessage = "maximum 50 element istifade oluna biler")]
        public string Client { get; set; }
        [Required(ErrorMessage = "Xana bos ola bilmez")]
        public Category? Category { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
