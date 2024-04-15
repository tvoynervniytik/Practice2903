using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2903.DB
{
    public partial class IngredientOfStage
    {
        public bool color
        { 
            get
            {
                if (TotalQuantity <= Ingredient.AvailableCount)
                    return true;
                else
                    return false;
            }

        }
        public double SumQuantity
        {

            get; set;
        }

        public double TotalQuantity { get; set; }
        public double TotalCost { get; set; }

    }
}
