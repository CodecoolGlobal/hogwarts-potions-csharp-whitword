using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
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
    
    public async Task<Potion> AddPotion(long id, List<IngredientDTO> ingredients)
    {
        Student student = _context.Students.Find(id);
        if (student == null) return null;
        Potion potion = new Potion();
        potion.Name = $"{student.Name}'s Potion";
        potion.Student = student;
        var contextIngredients = _context.Ingredients.ToList();
           foreach (var ingredient in ingredients)
           {
               {
                   var ingredientInDb = contextIngredients.FirstOrDefault(i=>i.ID == ingredient.ID);
                   if(ingredientInDb!=null) potion.Ingredients.Add(ingredientInDb);
               }
           }

        var recipes = 
            _context.Recipes
            .Include(recipe=>recipe.Ingredients)
            .Include(recipe=>recipe.Student)
            .ToList();
        var studentsRecipes = recipes.Where(recipe => recipe.Student.ID == id);
        foreach (var recipe in recipes)
        {
            var result = recipe.Ingredients.IntersectBy(potion.Ingredients.Select(x => x.ID), x => x.ID);
            if (result.Count() == recipe.Ingredients.Count()) potion.Recipe = recipe;
            break;
        }

        if (potion.Recipe == null)
        {
            string recipeName = $"{student.Name}'s Discovery #{studentsRecipes.Count()}";
            Recipe newRecipe = new Recipe
            {
                Name = recipeName,
                Student = student,
                Ingredients = potion.Ingredients
            };

            _context.Recipes.Add(newRecipe);
            await _context.SaveChangesAsync();
            potion.Recipe = _context.Recipes.FirstOrDefault(recipe => recipe.Name == recipeName);;
            potion.BrewingStatus = BrewingStatus.Discovery;
        }
        else
        {
             potion.BrewingStatus = BrewingStatus.Replica;
        }
        
        _context.Potions.Add(potion);
        await _context.SaveChangesAsync();
        return potion;
    }

    public Task<Potion> GetPotion(long potionId)
    {
        var potion = _context.Potions
            .Include(potion=>potion.Student)
            .Include(potion=>potion.Ingredients)
            .Include(potion=>potion.Recipe)
            .ThenInclude(recipe => recipe.Ingredients)
            .FirstOrDefaultAsync(potion=>potion.ID == potionId);
        return potion;
    }

    public Task<List<Potion>> GetAllPotions()
    {
        var potions = _context.Potions
            .Include(potion=>potion.Student)
            .Include(potion=>potion.Ingredients)
            .Include(potion=>potion.Recipe)
            .ThenInclude(r => r.Ingredients)
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