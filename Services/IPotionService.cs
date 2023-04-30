using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services;

public interface IPotionService
{
    Task<Potion> AddPotion(PotionDTO potion);
    Task<Potion> GetPotion(long potionId);
    Task<List<Potion>> GetAllPotions();
    Task<List<Potion>> GetPotionsPerStudent(long studentId);
    Task<Potion> UpdatePotion(long id, IngredientDTO ingredient);
    Task DeletePotion(long id);
    Task<List<Recipe>> Help(long id);
    List<Ingredient> ListAvailableIngredients();
}