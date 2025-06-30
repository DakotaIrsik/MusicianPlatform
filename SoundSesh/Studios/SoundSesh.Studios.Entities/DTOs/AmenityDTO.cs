namespace SoundSesh.Studios.Entities.DTOs
{
    public class AmenityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public string UserId { get; private set; }

    }
}
