using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services;

public interface IPotionService
{
    Task<Potion> AddPotion(long id, List<IngredientDTO> ingredients);
    Task<Potion> GetPotion(long potionId);
    Task<List<Potion>> GetAllPotions();
    Task UpdatePotion(Potion potion);
    Task DeletePotion(long id);
}