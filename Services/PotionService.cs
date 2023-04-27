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
    
    public async Task<Potion> AddPotion(PotionDTO requestPotionDto)
    {
        Student student = await _context.Students.FindAsync(requestPotionDto.StudentID);
        if (student == null) return null;
        Potion potion = new Potion
        {
            Name = $"{student.Name}'s Potion",
            Student = student
        };
        if (!requestPotionDto.Ingredients.Any())
        {
            potion.BrewingStatus = BrewingStatus.Brew;
            _context.Potions.Add(potion);
            await _context.SaveChangesAsync();
            return potion;
        }
        var contextIngredients = _context.Ingredients.ToList();
           foreach (var ingredient in requestPotionDto.Ingredients)
           {
               {
                   var ingredientInDb = contextIngredients.FirstOrDefault(i=>i.ID == ingredient.ID);
                   if(ingredientInDb!=null) potion.Ingredients.Add(ingredientInDb);
               }
           }
        
        if (RecipeChecker(potion) == null)
        {
            string recipeName = $"{student.Name}'s Discovery #{_context.Recipes.Count(recipe => recipe.Student.ID == student.ID)}";
            Recipe newRecipe = new Recipe
            {
                Name = recipeName,
                Student = student,
                Ingredients = potion.Ingredients
            };

            _context.Recipes.Add(newRecipe);
            await _context.SaveChangesAsync();
            potion.Recipe = newRecipe;
            potion.BrewingStatus = BrewingStatus.Discovery;
        }
        else
        {
            potion.Recipe = RecipeChecker(potion);
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

    public async Task<List<Potion>> GetAllPotions()
    {
        var potions = await _context.Potions
            .Include(potion=>potion.Student)
            .Include(potion=>potion.Ingredients)
            .Include(potion=>potion.Recipe)
            .ThenInclude(r => r.Ingredients)
            .ToListAsync();
        return potions;
    }
    public async Task<List<Potion>> GetPotionsPerStudent(long studentId)
    {
        var potionsPerStudent =await _context.Potions
            .Include(potion=>potion.Student)
            .Include(potion=>potion.Ingredients)
            .Include(potion=>potion.Recipe)
            .ThenInclude(r => r.Ingredients)
            .Where(potion => potion.Student.ID == studentId)
            .ToListAsync();
        return potionsPerStudent;
    }
    public Task UpdatePotion(Potion potion)
    {
        throw new System.NotImplementedException();
    }

    private Recipe RecipeChecker(Potion potion)
    {
        var recipes =
            _context.Recipes
                .Include(recipe => recipe.Ingredients)
                .ToList();
        foreach (var recipe in recipes)
        {
            var result1 = recipe.Ingredients.ExceptBy(potion.Ingredients.Select(x => x.ID), x => x.ID);
            var result2 = potion.Ingredients.ExceptBy(recipe.Ingredients.Select(x => x.ID), x => x.ID);

            if (!result1.Any() && !result2.Any())
            {
                return recipe;
            }

        }
        return null;
    }
    
    public Task DeletePotion(long id)
    {
        Task<Potion> potion = GetPotion(id);
        if (potion.Result != null)
        {
            _context.Potions.Remove(potion.Result);
            return _context.SaveChangesAsync();
        }
        return potion;
    }
}