using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice2903.DB
{
    public partial class Ingredient
    {
        public double PriceInDollars
        {
            get
            {
                return CostInCents * 0.01;
            }
        }
    }
}
