using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers;

[ApiController, Route("/potions")]
public class PotionController : ControllerBase
{
    private readonly IPotionService _potionService;

    public PotionController(IPotionService potionService)
    {
        _potionService = potionService;
    }
    
    [HttpGet]
    public async Task<List<Potion>> GetAllPotions()
    {
        return await _potionService.GetAllPotions();
    }
    [HttpPost("{id}")]
    public async Task<Potion> AddPotion(long id, [FromBody] List<IngredientDTO> ingredients)
    {
        return await _potionService.AddPotion(id, ingredients);
    }
}