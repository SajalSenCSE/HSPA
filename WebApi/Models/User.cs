using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class User:BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }

    }
}