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
            //var dishes = new List<Dish>()
            //{
            //    new Dish()
            //    {
            //        Name = "Chicken curry",
            //        Country = "India",
            //        Ingredients = new List<Ingredient>()
            //        {
            //            new Ingredient()
            //            {
            //                Name = "Chicken thigh",
            //                PricePerUnit = 2.50,
            //                Quantity = 2,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Brocolli",
            //                PricePerUnit = 1.00,
            //                Quantity = 1,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Cook cream",
            //                PricePerUnit = 1.00,
            //                Quantity = 1,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Garlic",
            //                PricePerUnit = 0.10,
            //                Quantity = 6,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Union",
            //                PricePerUnit = 0.25,
            //                Quantity = 1,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Indian mild curry powder",
            //                PricePerUnit = 0.25,
            //                Quantity = 0.25,
            //            }
            //        },
            //        Recipe = "Fruit de ui en de knoflook zacht..."
            //    },
            //    new Dish()
            //    {
            //        Name = "Rendang",
            //        Country = "Indonesia",
            //        Ingredients = new List<Ingredient>()
            //        {
            //            new Ingredient()
            //            {
            //                Name = "Beef",
            //                PricePerUnit = 5.00,
            //                Quantity = 1,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Beans",
            //                PricePerUnit = 2.00,
            //                Quantity = 3,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Cocos cream",
            //                PricePerUnit = 1.00,
            //                Quantity = 1,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Garlic",
            //                PricePerUnit = 0.10,
            //                Quantity = 6,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Union",
            //                PricePerUnit = 0.25,
            //                Quantity = 1,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Rendang paste",
            //                PricePerUnit = 2.00,
            //                Quantity = 1,
            //            }
            //        },
            //        Recipe = "Fruit de ui en de knoflook zacht..."
            //    },
            //    new Dish()
            //    {
            //        Name = "Pasta Bolognese",
            //        Country = "Italy",
            //        Ingredients = new List<Ingredient>()
            //        {
            //            new Ingredient()
            //            {
            //                Name = "Sausage",
            //                PricePerUnit = 2.50,
            //                Quantity = 8,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Tomatos",
            //                PricePerUnit = 1.00,
            //                Quantity = 10,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Red wine",
            //                PricePerUnit = 5.00,
            //                Quantity = 0.50,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Garlic",
            //                PricePerUnit = 0.10,
            //                Quantity = 6,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Union",
            //                PricePerUnit = 0.25,
            //                Quantity = 1,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Oregano",
            //                PricePerUnit = 0.25,
            //                Quantity = 0.25,
            //            },
            //            new Ingredient()
            //            {
            //                Name = "Basilicum",
            //                PricePerUnit = 0.25,
            //                Quantity = 0.25,
            //            }
            //        },
            //        Recipe = "Bak de saucijzen goed aan..."
            //    }
            //};

            //context.Dishes.AddRange(dishes);
            //context.SaveChanges();
        }
    }
}
