using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HogwartsPotions.Pages;

public class AddIngredient : PageModel
{
    private readonly IPotionService _potionService;

    public AddIngredient(IPotionService potionService)
    {
        _potionService = potionService;
    }

    [BindProperty]
    public PotionDTO PotionModel { get; set; }
    [BindProperty] 
    public List<SelectListItem> IngredientList { get; set; }

    public async Task<IActionResult> OnGet(long id)
    {
        IngredientList = new List<SelectListItem>();
            foreach (var listAvailableIngredient in _potionService.ListAvailableIngredients())
            {
                IngredientList.Add(new SelectListItem{Text = listAvailableIngredient.Name, Value = listAvailableIngredient.ID.ToString()});
            }
        var dbPotion = await _potionService.GetPotion(id);

        if (dbPotion == null)
        {
            return NotFound();
        }
        PotionModel = new PotionDTO()
        {
            ID = dbPotion.ID,
            Ingredients = new List<IngredientDTO>(){},
            BrewingStatus = dbPotion.BrewingStatus,
            Name = dbPotion.Name,
            StudentID = dbPotion.Student.ID
        } ;
        foreach (var potionModelIngredient in dbPotion.Ingredients)
        {
            PotionModel.Ingredients.Add(new IngredientDTO(){ID = potionModelIngredient.ID, Name = potionModelIngredient.Name});
        }
            
        ViewData["PotionModel"] = PotionModel;

        return Page();
        
    }

    [BindProperty] public IngredientDTO NewIngredient { get; set; } = new IngredientDTO();
    public async Task<IActionResult> OnPost(long id)
    {
        await _potionService.UpdatePotion(id, NewIngredient);
        return RedirectToPage();
    }
}