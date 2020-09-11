using MyDishesApp.Repository.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyDishesApp.Repository.Data
{
    public static class DishesContextExtensions
    {
        public static void EnsureSeedDataForContext(this DishesContext context)
        {
            // Check if this data is already in db
            if (context.Dishes.Any())
            {
                return;
            }

            // initial seed data
            var dishes = new List<Dish>()
            {
                new Dish
                {
                    Name = "Chicken curry",
                    Country = "India",
                    Recipe = "Fruit de ui en de knoflook zacht...",
                    DishIngredients = new List<DishIngredient>
                    {
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Chicken thigh",
                                PricePerUnit = 2.50,
                                Quantity = 2,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Brocolli",
                                PricePerUnit = 1.00,
                                Quantity = 1,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Cooking cream",
                                PricePerUnit = 1.00,
                                Quantity = 1,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Garlic",
                                PricePerUnit = 0.10,
                                Quantity = 6,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Union",
                                PricePerUnit = 0.25,
                                Quantity = 1,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Indian mild curry powder",
                                PricePerUnit = 0.25,
                                Quantity = 0.25,
                            }
                        }
                    }
                },
                new Dish
                {
                    Name = "Rendang",
                    Country = "Indonesië",
                    Recipe = "Fruit de ui en de knoflook zacht...",
                    DishIngredients = new List<DishIngredient>
                    {
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Beef",
                                PricePerUnit = 5.00,
                                Quantity = 1,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Beans",
                                PricePerUnit = 2.00,
                                Quantity = 3,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Cocos cream",
                                PricePerUnit = 1.00,
                                Quantity = 1,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Garlic",
                                PricePerUnit = 0.10,
                                Quantity = 6,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Union",
                                PricePerUnit = 0.25,
                                Quantity = 1,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Rendang paste",
                                PricePerUnit = 2.00,
                                Quantity = 1,
                            }
                        }
                    }
                },
                new Dish
                {
                    Name = "Pasta Bolognese",
                    Country = "Italy",
                    Recipe = "Bak de saucijzen goed aan...",
                    DishIngredients = new List<DishIngredient>
                    {
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Sausage",
                                PricePerUnit = 2.50,
                                Quantity = 8,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Tomatos",
                                PricePerUnit = 1.00,
                                Quantity = 10,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Red wine",
                                PricePerUnit = 5.00,
                                Quantity = 0.50,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Garlic",
                                PricePerUnit = 0.10,
                                Quantity = 6,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Union",
                                PricePerUnit = 0.25,
                                Quantity = 1,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Oregano",
                                PricePerUnit = 0.25,
                                Quantity = 0.25,
                            }
                        },
                        new DishIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Basilicum",
                                PricePerUnit = 0.25,
                                Quantity = 0.25,
                            }
                        }
                    }
                }
            };

            context.Dishes.AddRange(dishes);
            context.SaveChanges();
        }
    }
}
