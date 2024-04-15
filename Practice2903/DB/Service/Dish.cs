using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2903.DB
{
    public partial class Dish
    {
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
