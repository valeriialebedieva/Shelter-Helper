using ShelterHelper.Models;
using System.Text.Json;

namespace ShelterHelper.Services
{
    /// <summary>
    /// Service class for managing pet data and operations.
    /// Provides CRUD operations for pets in the shelter system.
    /// Pets are persisted to a JSON file for data retention across app restarts.
    /// </summary>
    public class PetService
    {
        /// <summary>
        /// In-memory collection of pets.
        /// </summary>
        private List<Pet> _pets = new();

        /// <summary>
        /// Path to the JSON file where pets are persisted.
        /// </summary>
        private readonly string _dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "pets.json");

        /// <summary>
        /// Constructor that initializes the service with sample pet data or loads existing data.
        /// </summary>
        public PetService()
        {
            LoadOrInitializePets();
        }

        /// <summary>
        /// Loads pets from the JSON file, or initializes with sample data if file doesn't exist.
        /// </summary>
        private void LoadOrInitializePets()
        {
            if (File.Exists(_dataFilePath))
            {
                try
                {
                    var json = File.ReadAllText(_dataFilePath);
                    _pets = JsonSerializer.Deserialize<List<Pet>>(json) ?? new List<Pet>();
                }
                catch
                {
                    InitializeSamplePets();
                }
            }
            else
            {
                InitializeSamplePets();
            }
        }

        /// <summary>
        /// Saves the current pet list to the JSON file.
        /// </summary>
        private void SavePets()
        {
            try
            {
                var dataDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }

                var json = JsonSerializer.Serialize(_pets, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_dataFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving pets: {ex.Message}");
            }
        }

        /// <summary>
        /// Initializes the collection with sample pets for first-time use.
        /// </summary>
        private void InitializeSamplePets()
        {
            _pets = new List<Pet>
            {
                new Pet
                {
                    Id = 1,
                    Name = "Max",
                    Species = "Dog",
                    Breed = "Golden Retriever",
                    Age = 3,
                    Notes = "Friendly and energetic, loves to play fetch. Great with kids!",
                    ImagePath = "https://images.dog.ceo/breeds/retriever-golden/n02099601_3004.jpg",
                    IsAdopted = false,
                    IsPetOfTheWeek = true
                },
                new Pet
                {
                    Id = 2,
                    Name = "Luna",
                    Species = "Cat",
                    Breed = "Siamese",
                    Age = 2,
                    Notes = "Playful and affectionate. Loves attention and interactive toys.",
                    ImagePath = "https://cdn2.thecatapi.com/images/N7rlRo9Zi.jpg",
                    IsAdopted = false,
                    IsPetOfTheWeek = false
                },
                new Pet
                {
                    Id = 3,
                    Name = "Charlie",
                    Species = "Dog",
                    Breed = "Labrador Mix",
                    Age = 5,
                    Notes = "Calm and gentle companion. Perfect for families looking for a loyal friend.",
                    ImagePath = "https://images.dog.ceo/breeds/labrador/john_walker_000.jpg",
                    IsAdopted = false,
                    IsPetOfTheWeek = false
                },
                new Pet
                {
                    Id = 4,
                    Name = "Whiskers",
                    Species = "Cat",
                    Breed = "Tabby",
                    Age = 1,
                    Notes = "Young and curious kitten. Still learning about the world around her.",
                    ImagePath = "https://cdn2.thecatapi.com/images/b6yBY91Pg.jpg",
                    IsAdopted = false,
                    IsPetOfTheWeek = false
                },
                new Pet
                {
                    Id = 5,
                    Name = "Buddy",
                    Species = "Dog",
                    Breed = "Beagle",
                    Age = 4,
                    Notes = "Sweet-natured and food-motivated. Great for training and outdoor adventures.",
                    ImagePath = "https://images.dog.ceo/breeds/beagle/puppy-1.jpg",
                    IsAdopted = false,
                    IsPetOfTheWeek = false
                },
                new Pet
                {
                    Id = 6,
                    Name = "Mittens",
                    Species = "Cat",
                    Breed = "Persian",
                    Age = 3,
                    Notes = "Elegant and serene. Prefers a quiet environment with lots of petting.",
                    ImagePath = "https://cdn2.thecatapi.com/images/c0f_dBlPH.jpg",
                    IsAdopted = false,
                    IsPetOfTheWeek = false
                }
            };
        }

        /// <summary>
        /// Retrieves all available pets (not adopted).
        /// </summary>
        /// <param name="species">Optional filter by species (e.g., "Dog", "Cat")</param>
        /// <returns>List of available pets matching the criteria</returns>
        public List<Pet> GetAvailablePets(string? species = null)
        {
            var query = _pets.Where(p => !p.IsAdopted);
            if (!string.IsNullOrEmpty(species))
                query = query.Where(p => p.Species == species);
            return query.ToList();
        }

        /// <summary>
        /// Retrieves the pet designated as Pet of the Week.
        /// </summary>
        /// <returns>Pet marked as Pet of the Week, or null if none exists</returns>
        public Pet? GetPetOfTheWeek() => _pets.FirstOrDefault(p => p.IsPetOfTheWeek);

        /// <summary>
        /// Adds a new pet to the system.
        /// Automatically assigns a unique ID based on current count.
        /// </summary>
        /// <param name="pet">The pet to add</param>
        public void AddPet(Pet pet)
        {
            pet.Id = _pets.Count > 0 ? _pets.Max(p => p.Id) + 1 : 1;
            _pets.Add(pet);
        }

        /// <summary>
        /// Updates an existing pet's information.
        /// </summary>
        /// <param name="updatedPet">The pet with updated information</param>
        public void UpdatePet(Pet updatedPet)
        {
            var index = _pets.FindIndex(p => p.Id == updatedPet.Id);
            if (index != -1)
                _pets[index] = updatedPet;
        }

        /// <summary>
        /// Deletes a pet from the system by ID.
        /// </summary>
        /// <param name="id">The ID of the pet to delete</param>
        public void DeletePet(int id) => _pets.RemoveAll(p => p.Id == id);

        /// <summary>
        /// Retrieves a specific pet by ID.
        /// </summary>
        /// <param name="id">The ID of the pet to retrieve</param>
        /// <returns>The pet with the specified ID, or null if not found</returns>
        public Pet? GetPetById(int id) => _pets.FirstOrDefault(p => p.Id == id);

        /// <summary>
        /// Retrieves all pets in the system (including adopted).
        /// </summary>
        /// <returns>List of all pets</returns>
        public List<Pet> GetAllPets() => _pets.ToList();
    }
}