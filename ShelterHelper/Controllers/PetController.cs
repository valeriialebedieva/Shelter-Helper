using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShelterHelper.Models;
using ShelterHelper.Services;

namespace ShelterHelper.Controllers;

/// <summary>
/// Controller for managing pet-related operations and views.
/// Handles CRUD operations for pets and pet search functionality.
/// </summary>
public class PetController : Controller
{
    private readonly PetService _petService;

    /// <summary>
    /// Initializes a new instance of the PetController.
    /// </summary>
    /// <param name="petService">The pet service for data operations</param>
    public PetController(PetService petService)
    {
        _petService = petService;
    }

    /// <summary>
    /// Displays a list of all pets with optional search filtering.
    /// </summary>
    /// <param name="searchString">Optional search term to filter pets by name</param>
    /// <returns>View with list of pets</returns>
    public IActionResult Index(string searchString)
    {
        var pets = _petService.GetAvailablePets().AsQueryable();

        // Search logic: Filter pets where name contains the search text
        if (!string.IsNullOrEmpty(searchString))
        {
            pets = pets.Where(s => s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        return View(pets.ToList());
    }

    /// <summary>
    /// Displays the create pet form.
    /// </summary>
    /// <returns>Create view</returns>
    [Authorize]
    [HttpGet]
    public IActionResult Create() => View();

    /// <summary>
    /// Handles the creation of a new pet with optional image upload.
    /// </summary>
    /// <param name="newPet">The pet data from the form</param>
    /// <param name="petPhoto">Optional image file for the pet</param>
    /// <returns>Redirects to Index on success, returns Create view on error</returns>
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Pet newPet, IFormFile petPhoto)
    {
        // Validate that image was provided
        if (petPhoto == null || petPhoto.Length == 0)
        {
            ModelState.AddModelError("petPhoto", "Pet photo is required");
        }

        if (!ModelState.IsValid)
        {
            return View(newPet);
        }

        try
        {
            // Handle image upload
            const long maxFileSize = 5 * 1024 * 1024; // 5MB max
            if (petPhoto.Length > maxFileSize)
            {
                ModelState.AddModelError("petPhoto", "File size cannot exceed 5MB");
                return View(newPet);
            }

            // Create a unique name for the image to avoid overwriting
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(petPhoto.FileName);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            
            // Ensure directory exists
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            string savePath = Path.Combine(imagePath, fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await petPhoto.CopyToAsync(stream);
            }

            newPet.ImagePath = "/images/" + fileName;

            _petService.AddPet(newPet);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            return View(newPet);
        }
    }

    /// <summary>
    /// Displays the edit form for a specific pet.
    /// </summary>
    /// <param name="id">The ID of the pet to edit</param>
    /// <returns>Edit view with pet data, or NotFound if pet doesn't exist</returns>
    [Authorize]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var pet = _petService.GetPetById(id);
        if (pet == null)
        {
            return NotFound();
        }
        return View(pet);
    }

    /// <summary>
    /// Handles the update of an existing pet.
    /// </summary>
    /// <param name="id">The ID of the pet to update</param>
    /// <param name="pet">The updated pet data</param>
    /// <param name="petPhoto">Optional new image file</param>
    /// <returns>Redirects to Index on success, returns Edit view on error</returns>
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Pet pet, IFormFile petPhoto)
    {
        if (id != pet.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(pet);
        }

        try
        {
            var existingPet = _petService.GetPetById(id);
            if (existingPet == null)
            {
                return NotFound();
            }

            // Handle new image upload
            if (petPhoto != null && petPhoto.Length > 0)
            {
                const long maxFileSize = 5 * 1024 * 1024; // 5MB max
                if (petPhoto.Length > maxFileSize)
                {
                    ModelState.AddModelError("petPhoto", "File size cannot exceed 5MB");
                    return View(pet);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(petPhoto.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                string savePath = Path.Combine(imagePath, fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await petPhoto.CopyToAsync(stream);
                }

                pet.ImagePath = "/images/" + fileName;
            }
            else
            {
                // Keep existing image if no new image provided
                pet.ImagePath = existingPet.ImagePath;
            }

            _petService.UpdatePet(pet);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            return View(pet);
        }
    }

    /// <summary>
    /// <summary>
    /// Displays delete confirmation for a pet.
    /// </summary>
    /// <param name="id">The ID of the pet to delete</param>
    /// <returns>Delete confirmation view, or NotFound if pet doesn't exist</returns>
    [Authorize]
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var pet = _petService.GetPetById(id);
        if (pet == null)
        {
            return NotFound();
        }
        return View(pet);
    }

    /// Handles the deletion of a pet.
    /// </summary>
    /// <param name="id">The ID of the pet to delete</param>
    /// <returns>Redirects to Index after deletion</returns>
    [Authorize]
    /// <returns>Redirects to Index after deletion</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            var pet = _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound();
            }

            // Delete associated image if it exists
            if (!string.IsNullOrEmpty(pet.ImagePath))
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", pet.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _petService.DeletePet(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"An error occurred while deleting the pet: {ex.Message}";
            return RedirectToAction("Index");
        }
    }
}