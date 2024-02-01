using System.ComponentModel.DataAnnotations;

namespace Agency.Areas.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Bu xana bosh ola bilmez")]
        [MaxLength(50,ErrorMessage ="Makximum 50 element istifade oluna biler")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [MaxLength(50, ErrorMessage = "Makximum 50 element istifade oluna biler")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [MaxLength(50, ErrorMessage = "Makximum 50 element istifade oluna biler")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [DataType(DataType.Password)]
        public string Password {  get; set; }
        [Required(ErrorMessage = "Bu xana bosh ola bilmez")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
