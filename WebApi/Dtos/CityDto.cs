using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is mantadory field")]
        [StringLength(20,MinimumLength =2)]
        [RegularExpression(".*[a-zA-Z]+.*",ErrorMessage ="Number is not Allow")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Country is required field")]
        public string Country { get; set; }
    }
}