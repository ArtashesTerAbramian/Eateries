using Eateries.Application.Interfaces;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Eateries.Infrastructure.Persistence.Services;

public class GenerateDishSuggestionAsync : IGenerateDishSuggestionAsync
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Order> _orders;

    public GenerateDishSuggestionAsync(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _orders = dbContext.Set<Order>();
    }

    // public async Task<List<Dish>> GenerateDishSuggestions(Guid userId, int numSuggestions)
    // {
    //     var allDishes = await _orders
    //         .Where(o => o.UserId == userId)
    //         .SelectMany(h => h.Dishes)
    //         .ToListAsync();
    //
    //     List<Cuisine> allCuisines = allDishes
    //         .Select(d => d.Cuisine)
    //         .Distinct()
    //         .ToList();
    //
    //     List<Ingredient> allIngredients = allDishes
    //         .SelectMany(d => d.DishIngredients)
    //         .Select(di => di.Ingredient)
    //         .Distinct()
    //         .ToList();
    //
    //     // Create a transition matrix for the Markov chain
    //     int numStates = allDishes.Count;
    //     double[,] transitionMatrix = new double[numStates, numStates];
    //     for (int i = 0; i < numStates; i++)
    //     {
    //         Dish currentDish = allDishes[i];
    //         var previousHistories = await _orders.Where(h => h.Dishes.Contains(currentDish)).ToListAsync();
    //         int numPrevious = previousHistories.Count;
    //         for (int j = 0; j < numStates; j++)
    //         {
    //             Dish nextDish = allDishes[j];
    //             int numNext = previousHistories.Count(h => h.Dishes.IndexOf(currentDish) < h.Dishes.Count - 1
    //                                                        && h.Dishes[h.Dishes.IndexOf(currentDish) + 1] == nextDish);
    //             transitionMatrix[i, j] = numNext / (double)numPrevious;
    //         }
    //     }
    //
    //     // Create a list of suggested dishes using the Markov chain
    //     List<Dish> suggestedDishes = new List<Dish>();
    //     int currentState = new Random().Next(numStates);
    //     while (suggestedDishes.Count < numSuggestions)
    //     {
    //         // Find the index of the next state using the transition probabilities
    //         double[] probabilities = new double[numStates];
    //         for (int j = 0; j < numStates; j++)
    //         {
    //             probabilities[j] = transitionMatrix[currentState, j];
    //         }
    //
    //         currentState = GetNextStateIndex(probabilities);
    //
    //         // Add the next dish to the list of suggestions
    //         Dish nextDish = allDishes[currentState];
    //         suggestedDishes.Add(nextDish);
    //     }
    //
    //     // Return the list of suggested dishes
    //     return suggestedDishes;
    // }
    //
    private int GetNextStateIndex(double[] probabilities)
    {
        double totalProbability = probabilities.Sum();
        double randomValue = new Random().NextDouble() * totalProbability;
        double cumulativeProbability = 0;
        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (cumulativeProbability >= randomValue)
            {
                return i;
            }
        }

        return probabilities.Length - 1;
    }

    public Task<List<Dish>> GenerateDishSuggestions(Guid userId, int numSuggestions)
    {
        throw new NotImplementedException();
    }
}