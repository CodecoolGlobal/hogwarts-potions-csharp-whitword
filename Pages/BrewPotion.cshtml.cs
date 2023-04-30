using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HogwartsPotions.Pages;

public class BrewPotion : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }
    [BindProperty]
        
    public PotionDTO Potion { get; set; }
    
}