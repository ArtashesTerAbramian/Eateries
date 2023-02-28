using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
using Eateries.Domain.Entities;
using Eateries.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Eateries.Infrastructure.Persistence.Services;

public class MarkovChainFoodSuggestorService : IMarkovChainFoodSuggestorService
{
    private readonly IOrderRepositoryAsync _orderRepositoryAsync;
    private readonly IDishRepositoryAsync _dishRepositoryAsync;

    public MarkovChainFoodSuggestorService(
        IOrderRepositoryAsync orderRepositoryAsync,
        IDishRepositoryAsync dishRepositoryAsync)
    {
        _orderRepositoryAsync = orderRepositoryAsync;
        _dishRepositoryAsync = dishRepositoryAsync;
    }

    public async Task<List<Dish>> SuggestFood(Guid userId, int numSuggestions)
    {
        var orderHistory = await _orderRepositoryAsync.GetOrdersByUserAsync(userId);
        var dishesOrdered = orderHistory
            .SelectMany(o => o.OrderDishes.Select(s => s.Dish))
            .ToList();

        // calculate frequency of subsequent dishes
        var transitionProbabilities = CalculateTransitionProbabilities(dishesOrdered);

        // generate food suggestion
        var currentDishId = dishesOrdered.Last().Id;
        var suggestedDishes = new List<Dish>();
        for (int i = 0; i < numSuggestions; i++)
        {
            var nextDishId = GetNextDishId(currentDishId, transitionProbabilities);
            var nextDish = await _dishRepositoryAsync.GetByIdAsync(nextDishId);
            suggestedDishes.Add(nextDish);
            currentDishId = nextDish.Id;
        }

        return suggestedDishes;
    }

    private Dictionary<Guid, Dictionary<Guid, double>> CalculateTransitionProbabilities(List<Dish> dishesOrdered)
    {
        var transitionCounts = new Dictionary<Guid, Dictionary<Guid, int>>();

        // calculate frequency of subsequent dishes
        for (int i = 0; i < dishesOrdered.Count - 1; i++)
        {
            var currentDish = dishesOrdered[i];
            var nextDish = dishesOrdered[i + 1];

            if (!transitionCounts.ContainsKey(currentDish.Id))
            {
                transitionCounts[currentDish.Id] = new Dictionary<Guid, int>();
            }

            if (!transitionCounts[currentDish.Id].ContainsKey(nextDish.Id))
            {
                transitionCounts[currentDish.Id][nextDish.Id] = 0;
            }

            transitionCounts[currentDish.Id][nextDish.Id]++;
        }

        // normalize counts to get probabilities
        var transitionProbabilities = new Dictionary<Guid, Dictionary<Guid, double>>();
        foreach (var currentDishId in transitionCounts.Keys)
        {
            var totalTransitions = transitionCounts[currentDishId].Values.Sum();
            transitionProbabilities[currentDishId] = new Dictionary<Guid, double>();
            foreach (var nextDishId in transitionCounts[currentDishId].Keys)
            {
                transitionProbabilities[currentDishId][nextDishId] =
                    (double)transitionCounts[currentDishId][nextDishId] / totalTransitions;
            }
        }

        return transitionProbabilities;
    }

    private Guid GetNextDishId(Guid currentDishId, Dictionary<Guid, Dictionary<Guid, double>> transitionProbabilities)
    {
        var possibleNextDishProbabilities = transitionProbabilities[currentDishId];

        // select a next dish based on probabilities
        var rand = new Random();
        var r = rand.NextDouble();
        var cumulativeProbability = 0.0;
        foreach (var nextDishProbability in possibleNextDishProbabilities)
        {
            cumulativeProbability += nextDishProbability.Value;
            if (r <= cumulativeProbability)
            {
                return nextDishProbability.Key;
            }
        }

        // if no dish is selected, return the first one in the list
        return possibleNextDishProbabilities.First().Key;
    }
}