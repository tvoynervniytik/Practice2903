using System;
using System.Collections.Generic;
using System.Linq;
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
using Practice2903.DB;

namespace Practice2903.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOfDishes.xaml
    /// </summary>
    public partial class ListOfDishes : Page
    {
        public static List<Dish> dishes { get; set; }
        public static List<Category> categories { get; set; }
        //ненужные таб
        //public static List<CookingStage> cookingStages { get; set; }
        //public static List<IngredientOfStage> ingredientOfStages { get; set; }
        //public static List<Ingredient> ingredients { get; set; }
        public static List<AvaibleIng> avaibleIngs { get; set; }
        public ListOfDishes()
        {
            InitializeComponent();
            dishes = new List<Dish>(DBConnection.practice.Dish.ToList());
            categories = new List<Category>(DBConnection.practice.Category.ToList());
            //cookingStages = new List<CookingStage>(DBConnection.practice.CookingStage.ToList());
            //ingredientOfStages = new List<IngredientOfStage>(DBConnection.practice.IngredientOfStage.ToList());
            //ingredients = new List<Ingredient>(DBConnection.practice.Ingredient.ToList());
            avaibleIngs = new List<AvaibleIng>(DBConnection.practice.AvaibleIng.Where(i => i.Quantity > i.AvailableCount).ToList());

            this.DataContext = this;
        }
        private void categoryCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cat = categoryCb.SelectedItem as Category;
            string name = nameTb.Text;
            if (categoryCb.SelectedItem == null)
                dishesSlv.ItemsSource = new List<Dish>(DBConnection.practice.Dish.Where(i => i.Name.StartsWith(name)).ToList());
            else
                dishesSlv.ItemsSource = new List<Dish>(DBConnection.practice.Dish.Where(i => i.Name.StartsWith(name) && i.CategoryId == cat.Id).ToList());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var cat = categoryCb.SelectedItem as Category;
            string name = nameTb.Text;
            dishesSlv.ItemsSource = new List<Dish>(DBConnection.practice.Dish.Where(i => i.Name.StartsWith(name) && i.CategoryId == cat.Id).ToList());
        }
        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            int count = avaibleIngs.Count; //кол-во элементов в Таблице sql
            int[] IdesDishes = new int[count]; //массив размера количества - массив с айдишниками блюд, где кол-ва ингредиентов не хватает
            int index = 0;
            while (index < count)
            {
                IdesDishes[index] = avaibleIngs[index].DishId;
                index++;
            }
             if ((bool)isChB.IsChecked)
               dishesSlv.ItemsSource = new List<Dish>(DBConnection.practice.Dish.Where(i => IdesDishes.Contains(i.Id) == false).ToList());
            else
                dishesSlv.ItemsSource = new List<Dish>(DBConnection.practice.Dish.ToList());
        }

        private void categoryBtn_Click(object sender, RoutedEventArgs e)
        {
            dishesSlv.ItemsSource = new List<Dish>(DBConnection.practice.Dish.ToList());
            categoryCb.SelectedItem = null;
        }

        private void dishesSlv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            NavigationService.Navigate(new RecipesPage(dishesSlv.SelectedItem as Dish));
        }
        private void priceSl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int a = (int)priceSl.Value;
            MessageBox.Show(a.ToString());
            //priceminTb.Text = a.ToString();
        }
        private void priceMSl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //int a = (int)priceMSl.Value;
            //pricemaxTb.Text = a.ToString();
        }
        
    }
}
