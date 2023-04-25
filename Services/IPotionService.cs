using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services;

public interface IPotionService
{
    Task AddPotion(Potion potion);
    Task<Potion> GetPotion(long potionId);
    Task<List<Potion>> GetAllPotions();
    Task UpdatePotion(Potion potion);
    Task DeletePotion(long id);
}