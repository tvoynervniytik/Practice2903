using Practice2903.DB;
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
            ingredients = new List<Ingredient>(DBConnection.practice.Ingredient.ToList());
            this.DataContext = this;
        }
    }
}
