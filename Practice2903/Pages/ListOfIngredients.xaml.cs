using Practice2903.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для ListOfIngredients.xaml
    /// </summary>
    public partial class ListOfIngredients : Page
    {
        public static List<Ingredient> ingredients {  get; set; }
        public ListOfIngredients()
        {
            InitializeComponent();
            Refresh();
        }
        private void HLDelete_Click(object sender, RoutedEventArgs e)
        {
            var ingredient = (sender as Hyperlink).DataContext as Ingredient;
            try
            {
                DBConnection.practice.Ingredient.Remove(ingredient);
                DBConnection.practice.SaveChanges();
            }
            catch
            {
                MessageBox.Show("This product cannot be removed");
            }

            Refresh();
        }
        private void TBСount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]");
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }
        private void Refresh()
        {
            var ingredients = DBConnection.practice.Ingredient.ToList();
            double result = 0;
            foreach (var i in ingredients)
            {
                double c = i.PriceInDollars * i.AvailableCount;
                result += c;
            }
            costTb.Text = result.ToString();
            LVIngredients.ItemsSource = ingredients;

        }
        private void BPlus_Click(object sender, RoutedEventArgs e)
        {
            var ingredient = (sender as Button).DataContext as Ingredient;
            if (ingredient.AvailableCount == 99)
                return;
            ingredient.AvailableCount += 1;
            DBConnection.practice.SaveChanges();
            Refresh();
        }
        private void BMinus_Click(object sender, RoutedEventArgs e)
        {
            var ingredient = (sender as Button).DataContext as Ingredient;
            try
            {
                if (ingredient.AvailableCount == 0)
                    return;
                ingredient.AvailableCount -= 1;
                DBConnection.practice.SaveChanges();

            }
            catch
            {
                MessageBox.Show("Error!");
            }

            Refresh();
        }
        private void TBСount_TextChanged(object sender, TextChangedEventArgs e)
        {
            DBConnection.practice.SaveChanges();
        }

    }
}
