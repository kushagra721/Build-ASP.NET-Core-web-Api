using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTOs
{
    public class updatewalkdto
    {
        [Required]
        [MinLength(3, ErrorMessage = "name has to be min of 3 chr")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "desc has to be min of 3 chr")]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        public string? walkImageurl { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }
    }
}
