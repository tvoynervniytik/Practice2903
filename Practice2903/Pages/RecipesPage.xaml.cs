using Practice2903.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practice2903.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecipesPage.xaml
    /// </summary>
    public partial class RecipesPage : Page
    {
        public static List<Dish> dishes { get; set; }
        public static List<Category> categories { get; set; }
        public static List<CookingStage> cookingStages { get; set; }
        public static List<IngredientOfStage> ingredientOfStages { get; set; }
        public static List<Ingredient> ingredients { get; set; }
        public static List<Unit> units { get; set; }
        private static Dish dishe { get; set; }
        
        int counter = 1;
        public RecipesPage(Dish dish)
        {
            InitializeComponent();
            dishe = dish;
            dishes = new List<Dish>(DBConnection.practice.Dish.ToList());
            categories = new List<Category>(DBConnection.practice.Category.ToList());
            cookingStages = new List<CookingStage>(DBConnection.practice.CookingStage.Where(i => i.DishId == dish.Id).ToList());
            int[] cookingStagesIdes = new int [cookingStages.Count];
            Refresh();
            int index = 0;
            int count = cookingStages.Count;
            ingredientOfStages = new List<IngredientOfStage>(DBConnection.practice.IngredientOfStage.ToList());
            nameTb.Text = dish.Name;
            var cat = categories.FirstOrDefault(i => i.Id == dish.CategoryId);
            categoryTb.Text = cat.Name;
            index = 0;
            int time = 0;
            while (index < cookingStages.Count)
            {
                time += (int)cookingStages[index].TimeInMinutes;
                index++;
            }
            cookingTb.Text = time.ToString();
            this.DataContext = this;
            Refresh();
        }

        private void plusBt_Click(object sender, RoutedEventArgs e)
        {
            if (dishe.BaseServingsQuantity == 99)
                return;
            counter++;
            Refresh();
        }

        private void minBt_Click(object sender, RoutedEventArgs e)
        {
            
                if (dishe.BaseServingsQuantity == 0)
            {
                return;
            }
                    
                if (counter > 1)
                {
                    counter--;
                }
                Refresh();
           
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListOfDishes());
        }
        private void Refresh()
        {
            DataContext = null;
            CookingStage.stepNumber = 1;
            DataContext = dishe;
            descTb.Text = dishe.Description;
            var t = dishe.CookingStage.SelectMany(s => s.IngredientOfStage).ToList();
            var v = new List<IngredientOfStage>();
            foreach (var i in t)
            {
                var w = v.Find(x => x.IngredientId == i.IngredientId);
                if (w == null)
                {
                    i.SumQuantity = (double)i.Quantity;
                    v.Add(i);
                }
                else
                {
                    w.SumQuantity += (double)i.Quantity;
                }

            }
            for (int i = 0; i < v.Count; i++)
            {
                v[i].TotalQuantity = v[i].SumQuantity * counter;
                v[i].TotalCost = v[i].Ingredient.PriceInDollars * v[i].TotalQuantity;
            }
            costTb.Text = $"{dishe.OurCost * counter}$";
            IngredientLv.ItemsSource = v;
            servTb.Text = (dishe.BaseServingsQuantity * counter).ToString();
            LVRecipesStep.ItemsSource = dishe.CookingStage.ToList();
        }
    }
}
