using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services;

public class PotionService : IPotionService
{
    public const int MaxIngredientsForPotions = 5;

    private readonly HogwartsContext _context;

    public PotionService(HogwartsContext context)
    {
        _context = context;
    }
    
    public Task AddPotion(Potion potion)
    {
        throw new System.NotImplementedException();
    }

    public Task<Potion> GetPotion(long potionId)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<Potion>> GetAllPotions()
    {
        var potions = _context.Potions
            .Include(potion => potion.Ingredients)
            .ToListAsync();
        return potions;
    }

    public Task UpdatePotion(Potion potion)
    {
        throw new System.NotImplementedException();
    }

    public Task DeletePotion(long id)
    {
        throw new System.NotImplementedException();
    }
}