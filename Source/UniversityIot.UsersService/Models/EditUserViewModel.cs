namespace UniversityIot.UsersService.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EditUserViewModel
    {
        [StringLength(10, MinimumLength = 10)]
        public string CustomerNumber { get; set; }
    }
}