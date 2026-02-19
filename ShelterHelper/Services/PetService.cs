using ShelterHelper.Models;

namespace ShelterHelper.Services
{
    /// <summary>
    /// Service class for managing pet data and operations.
    /// Provides CRUD operations for pets in the shelter system.
    /// </summary>
    public class PetService
    {
        /// <summary>
        /// In-memory collection of pets. In production, this would connect to a database.
        /// </summary>
        private List<Pet> _pets = new();

        /// <summary>
        /// Constructor that initializes the service with sample pet data.
        /// </summary>
        public PetService()
        {
            InitializeSamplePets();
        }

        /// <summary>
        /// Initializes the in-memory collection with sample pets for demonstration.
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
                    ImagePath = "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgZmlsbD0iIzg2NjYyMCIvPjxjaXJjbGUgY3g9IjIwMCIgY3k9IjEwMCIgcj0iNjAiIGZpbGw9IiNBMDgyNjAiLz48Y2lyY2xlIGN4PSIxNzAiIGN5PSI4MCIgcj0iMTUiIGZpbGw9IiMzMzMiLz48Y2lyY2xlIGN4PSIyMzAiIGN5PSI4MCIgcj0iMTUiIGZpbGw9IiMzMzMiLz48cGF0aCBkPSJNIDE5MCAxMzAgUSAxODAgMTQwIDE5MCAxNDUiIHN0cm9rZT0iIzMzMyIgc3Ryb2tlLXdpZHRoPSIyIiBmaWxsPSJub25lIi8+PHBhdGggZD0iTSAyMDAgMTMwIFEgMjIwIDE0MCAxOTAgMTQ1IiBzdHJva2U9IiMzMzMiIHN0cm9rZS13aWR0aD0iMiIgZmlsbD0ibm9uZSIvPjxyZWN0IHg9IjE2MCIgeT0iMTYwIiB3aWR0aD0iODAiIGhlaWdodD0iOTAiIGZpbGw9IiNBMDgyNjAiIHJ4PSIxMCIvPjxyZWN0IHg9IjE3MCIgeT0iMjQwIiB3aWR0aD0iMTIiIGhlaWdodD0iNDUiIGZpbGw9IiNBMDgyNjAiLz48cmVjdCB4PSIyMDUiIHk9IjI0MCIgd2lkdGg9IjEyIiBoZWlnaHQ9IjQ1IiBmaWxsPSIjQTA4MjYwIi8+PHRleHQgeD0iMjAwIiB5PSIyNzAiIGZvbnQtc2l6ZT0iMjQiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGZpbGw9IiNGRkYiPk1heCAtIEdvbGRlbiBSZXRyaWV2ZXI8L3RleHQ+PC9zdmc+",
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
                    ImagePath = "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgZmlsbD0iIzU2MzMzNSIvPjxjaXJjbGUgY3g9IjIwMCIgY3k9IjEyMCIgcj0iNTAiIGZpbGw9IiNFOURDQzAiLz48cG9seWdvbiBwb2ludHM9IjE2MCw2MCAxNTAsMzAgMTcwLDUwIiBmaWxsPSIjRTlEQ0MwIi8+PHBvbHlnb24gcG9pbnRzPSIyNDAsNjAgMjUwLDMwIDIzMCw1MCIgZmlsbD0iI0U5RENDMCIvPjxjaXJjbGUgY3g9IjE4MCIgY3k9IjExMCIgcj0iOCIgZmlsbD0iIzMzMyIvPjxjaXJjbGUgY3g9IjIyMCIgY3k9IjExMCIgcj0iOCIgZmlsbD0iIzMzMyIvPjxjaXJjbGUgY3g9IjIwMCIgY3k9IjEzNSIgcj0iNCIgZmlsbD0iI0ZGMzMzMyIvPjxwYXRoIGQ9Ik0gMjAwIDEzNSBRIDE5MCAxNDUgMTgwIDE0OCIgc3Ryb2tlPSIjRUU2NjY2IiBzdHJva2Utd2lkdGg9IjIiIGZpbGw9Im5vbmUiLz48cGF0aCBkPSJNIDIwMCAxMzUgUSAyMTAgMTQ1IDIyMCAxNDgiIHN0cm9rZT0iI0VFNjY2NiIgc3Ryb2tlLXdpZHRoPSIyIiBmaWxsPSJub25lIi8+PHJlY3QgeD0iMTYwIiB5PSIxNjAiIHdpZHRoPSI4MCIgaGVpZ2h0PSI4MCIgZmlsbD0iI0U5RENDMCIgcng9IjgiLz48cmVjdCB4PSIxNzAiIHk9IjI0MCIgd2lkdGg9IjEwIiBoZWlnaHQ9IjQwIiBmaWxsPSIjRTlEQ0MwIi8+PHJlY3QgeD0iMjA1IiB5PSIyNDAiIHdpZHRoPSIxMCIgaGVpZ2h0PSI0MCIgZmlsbD0iI0U5RENDMCIvPjx0ZXh0IHg9IjIwMCIgeT0iMjcwIiBmb250LXNpemU9IjIwIiB0ZXh0LWFuY2hvcj0ibWlkZGxlIiBmaWxsPSIjRkZGIj5MdW5hIC0gU2lhbWVzZTwvdGV4dD48L3N2Zz4=",
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
                    ImagePath = "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgZmlsbD0iIzg2NjYyMCIvPjxjaXJjbGUgY3g9IjIwMCIgY3k9IjExMCIgcj0iNjAiIGZpbGw9IiMzMzMiLz48Y2lyY2xlIGN4PSIxNzAiIGN5PSI4MCIgcj0iMTgiIGZpbGw9IiMzMzMiLz48Y2lyY2xlIGN4PSIyMzAiIGN5PSI4MCIgcj0iMTgiIGZpbGw9IiMzMzMiLz48Y2lyY2xlIGN4PSIxNjUiIGN5PSI3MCIgcj0iOCIgZmlsbD0iIzMzMyIvPjxjaXJjbGUgY3g9IjIzNSIgY3k9IjcwIiByPSI4IiBmaWxsPSIjMzMzIi8+PHBhdGggZD0iTSAxODUgMTQwIFEgMjAwIDE1MCAxODAgMTYwIiBzdHJva2U9IiMzMzMiIHN0cm9rZS13aWR0aD0iMiIgZmlsbD0ibm9uZSIvPjxwYXRoIGQ9Ik0gMjE1IDE0MCBRIDIwMCAxNTAgMjIwIDE2MCIgc3Ryb2tlPSIjMzMzIiBzdHJva2Utd2lkdGg9IjIiIGZpbGw9Im5vbmUiLz48cmVjdCB4PSIxNjAiIHk9IjE2MCIgd2lkdGg9IjgwIiBoZWlnaHQ9IjEwMCIgZmlsbD0iIzMzMyIgcng9IjEwIi8+PHJlY3QgeD0iMTcwIiB5PSIyNjAiIHdpZHRoPSIxMiIgaGVpZ2h0PSI0MCIgZmlsbD0iIzMzMyIvPjxyZWN0IHg9IjIwNSIgeT0iMjYwIiB3aWR0aD0iMTIiIGhlaWdodD0iNDAiIGZpbGw9IiMzMzMiLz48dGV4dCB4PSIyMDAiIHk9IjI3MCIgZm9udC1zaXplPSIxOCIgdGV4dC1hbmNob3I9Im1pZGRsZSIgZmlsbD0iI0ZGRiI+Q2hhcmxpZTwvdGV4dD48L3N2Zz4=",
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
                    ImagePath = "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgZmlsbD0iI0ZGQzMzMyIvPjxjaXJjbGUgY3g9IjIwMCIgY3k9IjEyMCIgcj0iNTAiIGZpbGw9IiNGRjkzNjYiLz48cG9seWdvbiBwb2ludHM9IjE2MCw2MCAxNTAsMzAgMTcwLDUwIiBmaWxsPSIjRkY5MzY2Ii8+PHBvbHlnb24gcG9pbnRzPSIyNDAsNjAgMjUwLDMwIDIzMCw1MCIgZmlsbD0iI0ZGOTM2NiIvPjxjaXJjbGUgY3g9IjE4MCIgY3k9IjExMCIgcj0iOCIgZmlsbD0iIzMzMyIvPjxjaXJjbGUgY3g9IjIyMCIgY3k9IjExMCIgcj0iOCIgZmlsbD0iIzMzMyIvPjxjaXJjbGUgY3g9IjIwMCIgY3k9IjEzNSIgcj0iNCIgZmlsbD0iI0ZGMzMzMyIvPjxwYXRoIGQ9Ik0gMjAwIDEzNSBRIDE5MCAxNDUgMTgwIDE0OCIgc3Ryb2tlPSIjRUU2NjY2IiBzdHJva2Utd2lkdGg9IjIiIGZpbGw9Im5vbmUiLz48cGF0aCBkPSJNIDIwMCAxMzUgUSAyMTAgMTQ1IDIyMCAxNDgiIHN0cm9rZT0iI0VFNjY2NiIgc3Ryb2tlLXdpZHRoPSIyIiBmaWxsPSJub25lIi8+PHJlY3QgeD0iMTYwIiB5PSIxNjAiIHdpZHRoPSI4MCIgaGVpZ2h0PSI4MCIgZmlsbD0iI0ZGOTM2NiIgcng9IjgiLz48bGluZSB4MT0iMTcwIiB5MT0iMTcwIiB4Mj0iMTcwIiB5Mj0iMjIwIiBzdHJva2U9IiNGRjMzMzMiIHN0cm9rZS13aWR0aD0iMiIvPjxyZWN0IHg9IjE3MCIgeT0iMjQwIiB3aWR0aD0iMTAiIGhlaWdodD0iNDAiIGZpbGw9IiNGRjkzNjYiLz48cmVjdCB4PSIyMDUiIHk9IjI0MCIgd2lkdGg9IjEwIiBoZWlnaHQ9IjQwIiBmaWxsPSIjRkY5MzY2Ii8+PHRleHQgeD0iMjAwIiB5PSIyNzAiIGZvbnQtc2l6ZT0iMTgiIHRleHQtYW5jaG9yPSJtaWRkbGUiIGZpbGw9IiMzMzMiPldobpc3BlcnM8L3RleHQ+PC9zdmc+",
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
                    ImagePath = "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgZmlsbD0iIzZCNDUyMyIvPjxjaXJjbGUgY3g9IjIwMCIgY3k9IjExMCIgcj0iNTUiIGZpbGw9IiNBNjY5NDEiLz48cmVjdCB4PSIxNzAiIHk9IjYwIiB3aWR0aD0iMzAiIGhlaWdodD0iMzUiIGZpbGw9IiMzMzMiIHJ4PSIxMCIvPjxyZWN0IHg9IjIwMCIgeT0iNjAiIHdpZHRoPSIzMCIgaGVpZ2h0PSIzNSIgZmlsbD0iIzMzMyIgcng9IjEwIi8+PGNpcmNsZSBjeD0iMTgwIiBjeT0iOTAiIHI9IjgiIGZpbGw9IiMzMzMiLz48Y2lyY2xlIGN4PSIyMjAiIGN5PSI5MCIgcj0iOCIgZmlsbD0iIzMzMyIvPjxjaXJjbGUgY3g9IjIwMCIgY3k9IjEzNSIgcj0iNiIgZmlsbD0iIzMzMyIvPjxwYXRoIGQ9Ik0gMjAwIDEzNSBRIDE5MCAxNDUgMTgwIDE0OCIgc3Ryb2tlPSIjMzMzIiBzdHJva2Utd2lkdGg9IjIiIGZpbGw9Im5vbmUiLz48cGF0aCBkPSJNIDIwMCAxMzUgUSAyMTAgMTQ1IDIyMCAxNDgiIHN0cm9rZT0iIzMzMyIgc3Ryb2tlLXdpZHRoPSIyIiBmaWxsPSJub25lIi8+PHJlY3QgeD0iMTYwIiB5PSIxNjAiIHdpZHRoPSI4MCIgaGVpZ2h0PSI5MCIgZmlsbD0iI0E2Njk0MSIgcng9IjEwIi8+PHJlY3QgeD0iMTcwIiB5PSIyNTAiIHdpZHRoPSIxMiIgaGVpZ2h0PSI0MCIgZmlsbD0iI0E2Njk0MSIvPjxyZWN0IHg9IjIwNSIgeT0iMjUwIiB3aWR0aD0iMTIiIGhlaWdodD0iNDAiIGZpbGw9IiNBNjY5NDEiLz48dGV4dCB4PSIyMDAiIHk9IjI3MCIgZm9udC1zaXplPSIyMCIgdGV4dC1hbmNob3I9Im1pZGRsZSIgZmlsbD0iI0ZGRiI+QnVkZHk8L3RleHQ+PC9zdmc+",
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
                    ImagePath = "data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB3aWR0aD0iNDAwIiBoZWlnaHQ9IjMwMCIgZmlsbD0iI0Y1RjVEQyIvPjxjaXJjbGUgY3g9IjIwMCIgY3k9IjEyMCIgcj0iNTUiIGZpbGw9IiNFMEQyQjAiLz48cG9seWdvbiBwb2ludHM9IjE2MCw2MCAxNDUsMjAgMTcwLDUwIiBmaWxsPSIjRTBEMkIwIi8+PHBvbHlnb24gcG9pbnRzPSIyNDAsNjAgMjU1LDIwIDIzMCw1MCIgZmlsbD0iI0UwRDJCMCIvPjxjaXJjbGUgY3g9IjE3NSIgY3k9IjEwNSIgcj0iOSIgZmlsbD0iIzMzMyIvPjxjaXJjbGUgY3g9IjIyNSIgY3k9IjEwNSIgcj0iOSIgZmlsbD0iIzMzMyIvPjxjaXJjbGUgY3g9IjIwMCIgY3k9IjEzNSIgcj0iNSIgZmlsbD0iI0ZGNjY2NiIvPjxwYXRoIGQ9Ik0gMjAwIDEzNSBRIDE5MCAxNDUgMTgwIDE0OCIgc3Ryb2tlPSIjRkY2NjY2IiBzdHJva2Utd2lkdGg9IjIiIGZpbGw9Im5vbmUiLz48cGF0aCBkPSJNIDIwMCAxMzUgUSAyMTAgMTQ1IDIyMCAxNDgiIHN0cm9rZT0iI0ZGNjY2NiIgc3Ryb2tlLXdpZHRoPSIyIiBmaWxsPSJub25lIi8+PGVsbGlwc2UgY3g9IjIwMCIgY3k9IjE3MCIgcng9IjcwIiByeT0iODUiIGZpbGw9IiNFMEQyQjAiLz48cmVjdCB4PSIxNzAiIHk9IjI1MCIgd2lkdGg9IjEwIiBoZWlnaHQ9IjQwIiBmaWxsPSIjRTBEMkIwIi8+PHJlY3QgeD0iMjA1IiB5PSIyNTAiIHdpZHRoPSIxMCIgaGVpZ2h0PSI0MCIgZmlsbD0iI0UwRDJCMCIvPjx0ZXh0IHg9IjIwMCIgeT0iMjcwIiBmb250LXNpemU9IjE4IiB0ZXh0LWFuY2hvcj0ibWlkZGxlIiBmaWxsPSIjMzMzIj5NaXR0ZW5zPC90ZXh0Pjwvc3ZnPg==",
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