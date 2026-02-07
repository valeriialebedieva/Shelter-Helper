using Microsoft.AspNetCore.Mvc;
using ShelterHelper.Models;

public class PetController : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Pet newPet)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(newPet);
    }
}