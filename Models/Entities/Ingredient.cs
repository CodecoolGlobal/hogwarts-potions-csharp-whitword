using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HogwartsPotions.Models.Entities;
public class Ingredient
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ID { get; set; }
    
    public string Name { get; set; } = null!;
    [JsonIgnore]
    public List<Potion> PotionList { get; set; } = null;
    [JsonIgnore]
    public List<Recipe> RecipeList { get; set; } = null;

}
public class IngredientDTO
{
    public long ID { get; set; }
    
    public string Name { get; set; } = null!;
}