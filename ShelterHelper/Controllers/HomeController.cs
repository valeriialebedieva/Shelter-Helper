using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShelterHelper.Models;
using ShelterHelper.Services;

namespace ShelterHelper.Controllers;

public class HomeController : Controller
{
    private readonly PetService _petService;

    // Add this constructor to "inject" the service
    public HomeController(PetService petService)
    {
        _petService = petService;
    }

 public IActionResult Index(string searchString)
        {
            // This line will now work
            var pets = _petService.GetAvailablePets(); 

            if (!string.IsNullOrEmpty(searchString))
            {
                pets = pets.Where(s => s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.PetOfTheWeek = _petService.GetPetOfTheWeek();

            return View(pets);
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
