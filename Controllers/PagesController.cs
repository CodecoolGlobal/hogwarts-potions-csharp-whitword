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
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult BrewPotion()
    {
        return View();
    }
    [BindProperty]
    public Potion PotionModel { get; set; }
    [BindProperty]
    public IngredientDTO NewIngredient { get; set; }

    public async Task<ViewResult> AddIngredient(long id)
    {
        PotionModel = await _potionService.GetPotion(id);
        NewIngredient = new IngredientDTO();

        ViewData["PotionModel"] = PotionModel;
        ViewData["NewIngredient"] = NewIngredient;

        return View();
    }

    public async Task<IActionResult> DeletePotion(long id)
    {
        await _potionService.DeletePotion(id);
        return RedirectToPage("/Index");
    }
    [HttpPost("/AddIngredient/{id}")]
    public async Task<IActionResult> UpdatePotion(long id, IngredientDTO ingredient)
    {
        await _potionService.UpdatePotion(id, ingredient);
        return RedirectToPage("/Index");
    }
    [HttpPost]
    public async Task<IActionResult> BrewPotion(PotionDTO potion)
    {
        await _potionService.AddPotion(potion);
        return RedirectToPage("/Index");
    }
}