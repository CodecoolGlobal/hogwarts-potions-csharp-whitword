using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HogwartsPotions.Models.Entities;

public class Recipe
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ID { get; set; }
    
    public string Name { get; set; } = null!;
    
    [JsonIgnore]
    public Student? Student { get; set; }

    
    public HashSet<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();

}