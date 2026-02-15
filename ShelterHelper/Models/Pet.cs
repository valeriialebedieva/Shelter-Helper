namespace ShelterHelper.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; } 
        public string Breed { get; set; }
        public int Age { get; set; }
        public string Notes { get; set; }
        public string? ImagePath { get; set; } 
        public bool IsAdopted { get; set; } = false;
    }
}