using System.Collections.Generic;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities;

public class PotionDTO
{
    public long ID { get; set; }

    public long StudentID { get; set; }
    
    public string Name { get; set; } = null!;
    public BrewingStatus BrewingStatus { get; set; }

    public List<IngredientDTO> Ingredients { get; set; } = new List<IngredientDTO>();
}