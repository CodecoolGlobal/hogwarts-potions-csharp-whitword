using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;


namespace HogwartsPotions.Controllers;

public class PagesController : Controller
{
    private readonly IPotionService _potionService;

    public PagesController(IPotionService potionService)
    {
        _potionService = potionService;
    }
    // GET
    public async Task<IActionResult> DeletePotion(long id)
    {
        await _potionService.DeletePotion(id);
        return RedirectToPage("/Index");
    }

    public async Task<IActionResult> DeleteIngredient(long potionId, long ingredientId)
    {
        await _potionService.RemoveIngredientFromPotion(potionId, ingredientId);
        return RedirectToPage("/Index");
    }
    [HttpPost]
    public async Task<IActionResult> BrewPotion(PotionDTO potion)
    {
        await _potionService.AddPotion(potion);
        return RedirectToPage("/Index");
    }
}