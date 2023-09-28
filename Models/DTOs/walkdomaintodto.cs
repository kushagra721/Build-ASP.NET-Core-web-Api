namespace NZwalks.API.Models.DTOs
{
    public class walkdomaintodto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? walkImageurl { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }

    }
}
