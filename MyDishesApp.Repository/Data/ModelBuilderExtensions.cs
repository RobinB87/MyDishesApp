using Microsoft.EntityFrameworkCore;
using MyDishesApp.Repository.Data.Entities;

namespace MyDishesApp.Repository.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedDatabase(this ModelBuilder modelBuilder)
        {
            var dishes = new Dish[]
            {
                new Dish
                {
                    DishId = 1,
                    Name = "Chicken curry",
                    Country = "India",
                    Recipe = "Fruit de ui en de knoflook zacht...",
                },

                new Dish
                {
                    DishId = 2,
                    Name = "Rendang",
                    Country = "Indonesië",
                    Recipe = "Fruit de ui en de knoflook zacht..."
                },
                new Dish
                {
                    DishId = 3,
                    Name = "Pasta Bolognese",
                    Country = "Italy",
                    Recipe = "Bak de saucijzen goed aan..."
                }
            };

            var ingredients = new Ingredient[]
            {
                new Ingredient
                {
                    IngredientId = 1,
                    Name = "Chicken thigh",
                    PricePerUnit = 2.50
                },
                new Ingredient
                {
                    IngredientId = 2,
                    Name = "Brocolli",
                    PricePerUnit = 1.00
                },
                new Ingredient
                {
                    IngredientId = 3,
                    Name = "Cooking cream",
                    PricePerUnit = 1.00
                },
                new Ingredient
                {
                    IngredientId = 4,
                    Name = "Garlic",
                    PricePerUnit = 0.10
                },
                new Ingredient
                {
                    IngredientId = 5,
                    Name = "Union",
                    PricePerUnit = 0.25
                },
                new Ingredient
                {
                    IngredientId = 6,
                    Name = "Indian mild curry powder",
                    PricePerUnit = 0.25
                },
                new Ingredient
                {
                    IngredientId = 7,
                    Name = "Beef",
                    PricePerUnit = 5.00
                },
                new Ingredient
                {
                    IngredientId = 8,
                    Name = "Beans",
                    PricePerUnit = 2.00
                },
                new Ingredient
                {
                    IngredientId = 9,
                    Name = "Cocos cream",
                    PricePerUnit = 1.00
                },
                new Ingredient
                {
                    IngredientId = 10,
                    Name = "Rendang paste",
                    PricePerUnit = 2.00
                },
                new Ingredient
                {
                    IngredientId = 11,
                    Name = "Sausage",
                    PricePerUnit = 2.50
                },
                new Ingredient
                {
                    IngredientId = 12,
                    Name = "Tomatos",
                    PricePerUnit = 1.00
                },
                new Ingredient
                {
                    IngredientId = 13,
                    Name = "Red wine",
                    PricePerUnit = 5.00
                },
                new Ingredient
                {
                    IngredientId = 14,
                    Name = "Oregano",
                    PricePerUnit = 0.25
                },
                new Ingredient
                {
                    IngredientId = 15,
                    Name = "Basilicum",
                    PricePerUnit = 0.25
                }
            };

            var dishIngredients = new DishIngredient[]
            {
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 1,
                    Quantity = 2
                },
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 2,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 3,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 4,
                    Quantity = 6
                },
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 5,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 1,
                    IngredientId = 6,
                    Quantity = 0.25
                },
                new DishIngredient
                {
                    DishId = 2,
                    IngredientId = 7,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 2,
                    IngredientId = 8,
                    Quantity = 3
                },
                new DishIngredient
                {
                    DishId = 2,
                    IngredientId = 9,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 2,
                    IngredientId = 10,
                    Quantity = 1
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 11,
                    Quantity = 8
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 12,
                    Quantity = 10
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 13,
                    Quantity = 0.50
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 14,
                    Quantity = 0.25
                },
                new DishIngredient
                {
                    DishId = 3,
                    IngredientId = 15,
                    Quantity = 0.25
                }
            };

            modelBuilder.Entity<Dish>().HasData(dishes[0], dishes[1], dishes[2]);
            modelBuilder.Entity<Ingredient>().HasData(ingredients[0], ingredients[1], ingredients[2], ingredients[3], ingredients[4], ingredients[5], ingredients[6], ingredients[7], ingredients[8], ingredients[9], ingredients[10], ingredients[10], ingredients[11], ingredients[12], ingredients[13]);
            modelBuilder.Entity<DishIngredient>().HasData(dishIngredients[0], dishIngredients[1], dishIngredients[2], dishIngredients[3], dishIngredients[4], dishIngredients[5], dishIngredients[6], dishIngredients[7], dishIngredients[8], dishIngredients[9], dishIngredients[10], dishIngredients[10], dishIngredients[11], dishIngredients[12], dishIngredients[13]);
        }
    }
}