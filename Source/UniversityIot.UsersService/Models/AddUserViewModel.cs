namespace UniversityIot.UsersService.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AddUserViewModel
    {
        [Required]
        public string Name { get; set; }

        [StringLength(10, MinimumLength = 10)]
        public string CustomerNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}