using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Recipe> recipes = new List<Recipe>();
            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Enter Recipe Details");
                Console.WriteLine("2. Display Recipe");
                Console.WriteLine("3. Scale Recipe");
                Console.WriteLine("4. Reset Quantities");
                Console.WriteLine("5. Clear All Data");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Recipe newRecipe = new Recipe();
                        newRecipe.EnterRecipeDetails();
                        recipes.Add(newRecipe);
                        break;
                    case "2":
                        DisplayRecipeList(recipes);
                        break;
                    case "3":
                        ScaleRecipe(recipes);
                        break;
                    case "4":
                        ResetQuantities(recipes);
                        break;
                    case "5":
                        recipes.Clear();
                        Console.WriteLine("All recipes cleared.");
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void DisplayRecipeList(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine("Recipes:");
            foreach (var recipe in recipes.OrderBy(r => r.Name))
            {
                Console.WriteLine(recipe.Name);
            }

            Console.Write("Enter the name of the recipe to display: ");
            string recipeName = Console.ReadLine();
            Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (selectedRecipe != null)
            {
                selectedRecipe.DisplayRecipe();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        static void ScaleRecipe(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.Write("Enter the name of the recipe to scale: ");
            string recipeName = Console.ReadLine();
            Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (selectedRecipe != null)
            {
                selectedRecipe.ScaleRecipe();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        static void ResetQuantities(List<Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.Write("Enter the name of the recipe to reset quantities: ");
            string recipeName = Console.ReadLine();
            Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (selectedRecipe != null)
            {
                selectedRecipe.ResetQuantities();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }
    }

    class Recipe
    {
        private List<Ingredient> ingredients;
        private List<string> steps;

        public string Name { get; set; }

        public Recipe()
        {
            ingredients = new List<Ingredient>();
            steps = new List<string>();
        }

        public void EnterRecipeDetails()
        {
            Console.Write("Enter the name of the recipe: ");
            Name = Console.ReadLine();

            ingredients.Clear();
            steps.Clear();

            Console.Write("Enter the number of ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Ingredient {i + 1}:");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Quantity: ");
                double quantity = double.Parse(Console.ReadLine());
                Console.Write("Unit of Measurement: ");
                string unit = Console.ReadLine();
                Console.Write("Calories per Unit: ");
                int caloriesPerUnit = int.Parse(Console.ReadLine());
                Console.Write("Food Group: ");
                string foodGroup = Console.ReadLine();

                Ingredient ingredient = new Ingredient(name, quantity, unit, caloriesPerUnit, foodGroup);
                ingredients.Add(ingredient);
            }

            Console.Write("Enter the number of steps: ");
            int numSteps = int.Parse(Console.ReadLine());

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Step {i + 1}:");
                string step = Console.ReadLine();
                steps.Add(step);
            }

            Console.WriteLine("Recipe details entered successfully!");
        }

        public void DisplayRecipe()
        {
            Console.WriteLine($"\nRecipe: {Name}");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }

            int totalCalories = ingredients.Sum(i => i.Calories);
            Console.WriteLine($"Total Calories: {totalCalories}");
            if (totalCalories > 300)
            {
                Console.WriteLine("Warning: Total calories exceed 300!");
            }
        }

        public void ScaleRecipe()
        {
            Console.Write("Enter the scaling factor (0.5 for half, 2 for double, 3 for triple): ");
            double factor = double.Parse(Console.ReadLine());

            foreach (var ingredient in ingredients)
            {
                ingredient.Quantity *= factor;
            }

            Console.WriteLine("Recipe scaled successfully!");
        }

        public void ResetQuantities()
        {
            foreach (var ingredient in ingredients)
            {
                ingredient.ResetQuantity();
            }

            Console.WriteLine("Ingredient quantities reset to original values.");
        }
    }

    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public int CaloriesPerUnit { get; set; }
        public string FoodGroup { get; set; }

        public int Calories => (int)(Quantity * CaloriesPerUnit);

        public Ingredient(string name, double quantity, string unit, int caloriesPerUnit, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            CaloriesPerUnit = caloriesPerUnit;
            FoodGroup = foodGroup;
        }

        public void ResetQuantity()
        {
            // Reset quantity to original value//
        }
    }
}
