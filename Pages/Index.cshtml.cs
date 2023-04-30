using System.Collections.Generic;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HogwartsPotions.Pages;

public class Index : PageModel
{
    private IPotionService _potionService;
    [BindProperty]
    public IEnumerable<Potion>? PotionList { get; set; }

    public Index(IPotionService potionService)
    {
        _potionService = potionService;
    }
    public IActionResult OnGet()
    {
        PotionList = _potionService.GetAllPotions().Result;
        if (PotionList == null)
        {
            return NotFound();
        }
        return Page();
    }
    
}