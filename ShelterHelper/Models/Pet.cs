namespace ShelterHelper.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; } // Dog or Cat
        public string Breed { get; set; }
        public int Age { get; set; }
        public string Notes { get; set; }
        public bool IsAdopted { get; set; } = false;
    }
}