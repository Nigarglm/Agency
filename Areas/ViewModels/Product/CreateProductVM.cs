using System.ComponentModel.DataAnnotations;
using Agency.Models;

namespace Agency.Areas.ViewModels.Product
{
    public class CreateProductVM
    {
        [Required(ErrorMessage ="Xana bos ola bilmez")]
        [MaxLength(50,ErrorMessage ="maximum 50 element istifade oluna biler")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Xana bos ola bilmez")]
        [MaxLength(300, ErrorMessage = "maximum 300 element istifade oluna biler")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Xana bos ola bilmez")]
        [MaxLength(50, ErrorMessage = "maximum 50 element istifade oluna biler")]
        public string Client { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
