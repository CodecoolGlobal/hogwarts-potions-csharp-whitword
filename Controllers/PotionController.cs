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
    [HttpGet("{id}")]
    public async Task<List<Potion>> GetPotionsPerStudent(long id)
    {
        return await _potionService.GetPotionsPerStudent(id);
    }
    [HttpGet("/potions/{id}/help")]
    public async Task<List<Recipe>> Help(long id)
    {
        return await _potionService.Help(id);
    }
    [HttpPost]
    public async Task<Potion> AddPotion([FromBody] PotionDTO potion)
    {
        return await _potionService.AddPotion(potion);
    }
    [HttpPost("/potions/brew")]
    public async Task<Potion> BrewPotion([FromBody] PotionDTO potion)
    {
        return await _potionService.AddPotion(potion);
    }
    [HttpPut("/potions/{id}/add")]
    public async Task<Potion> UpdatePotion(long id, [FromBody] IngredientDTO ingredient)
    {
        return await _potionService.UpdatePotion(id, ingredient);
    }
    [HttpDelete("{id}")]
    public async Task DeletePotion(long id)
    {
        await _potionService.DeletePotion(id);
    }
}