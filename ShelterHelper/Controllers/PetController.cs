using Microsoft.AspNetCore.Mvc;
using ShelterHelper.Models;

public class PetController : Controller
{
    // Mock database for now
    private static List<Pet> _pets = new List<Pet>();

    // GET: /Pet/Index?searchString=Buddy
    public IActionResult Index(string searchString)
    {
        var pets = _pets.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            // Search logic: Filter pets where name contains the search text
            pets = pets.Where(s => s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        return View(pets.ToList());
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Pet newPet, IFormFile petPhoto)
    {
        if (petPhoto != null && petPhoto.Length > 0)
        {
            // Create a unique name for the image to avoid overwriting
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(petPhoto.FileName);
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await petPhoto.CopyToAsync(stream);
            }

            newPet.ImagePath = "/images/" + fileName; // Save the path for the View
        }

        _pets.Add(newPet);
        return RedirectToAction("Index");
    }
}