using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2903.DB
{
    public partial class Dish
    {
        public string OpacityDish
        {
            get
            {
                var destinationFormat = string.Empty;
                var allIngredientRecipeSteps = this.CookingStage.SelectMany(x => x.IngredientOfStage);
                if (allIngredientRecipeSteps.Any())
                {
                    foreach (var ingredientStep in allIngredientRecipeSteps)
                    {
                        if (allIngredientRecipeSteps.Where(x => x.IngredientId == ingredientStep.IngredientId).Sum(x => x.Quantity) > ingredientStep.Ingredient.AvailableCount)
                        {
                            destinationFormat = "Gray32Float";
                            IsAvailable = false;
                            DBConnection.practice.SaveChanges();
                        }
                    }
                }
                else
                {
                    destinationFormat = "Bgra32";
                    IsAvailable = true;
                    DBConnection.practice.SaveChanges();
                }
                return destinationFormat;
            }
        }
        public double OurCost
        {
            get
            {

                var v = this.CookingStage.SelectMany(s => s.IngredientOfStage).ToList();
                double result = 0;
                foreach (var i in v)
                {
                    result += (double)(i.Ingredient.PriceInDollars * i.Quantity);
                }
                return result;

            }
        }
    }
}
