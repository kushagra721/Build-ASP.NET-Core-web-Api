using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTOs
{
    public class registerDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string[] Roles { get; set; }

    }
}
