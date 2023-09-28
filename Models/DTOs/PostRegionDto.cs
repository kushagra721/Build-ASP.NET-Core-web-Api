using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTOs
{
    public class PostRegionDto
    {

        [Required]
        [MinLength(3,ErrorMessage ="code has to be minimum of 3 char")]
        [MaxLength(3,ErrorMessage ="code has to be max of 3 char")]
        public string Code { get; set; }

        [Required]
        [MinLength(3,ErrorMessage ="name has to be min of 3 char")]
        [MaxLength(26,ErrorMessage ="name has to be max of 26 char")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }


    }
}
