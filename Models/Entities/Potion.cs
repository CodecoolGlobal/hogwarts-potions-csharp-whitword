using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities;

public class Potion
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ID { get; set; }
    
    public string Name { get; set; } = null!;
    
    [JsonIgnore]
    public Student? Student { get; set; }
    
    public HashSet<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
    
    public BrewingStatus BrewingStatus { get; set; }
    
    [JsonIgnore]
    public Recipe Recipe { get; set; }

}