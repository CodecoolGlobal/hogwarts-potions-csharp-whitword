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
    [HttpPost]
    public async Task<Potion> AddPotion([FromBody] PotionDTO potion)
    {
        return await _potionService.AddPotion(potion);
    }
    [HttpDelete("{id}")]
    public async Task DeletePotion(long id)
    {
        await _potionService.DeletePotion(id);
    }
}