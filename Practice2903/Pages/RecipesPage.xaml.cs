﻿using Practice2903.DB;
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
        public RecipesPage(Dish dish)
        {
            InitializeComponent();
            dishes = new List<Dish>(DBConnection.practice.Dish.ToList());
            categories = new List<Category>(DBConnection.practice.Category.ToList());
            cookingStages = new List<CookingStage>(DBConnection.practice.CookingStage.Where(i => i.DishId == dish.Id).ToList());
            int[] cookingStagesIdes = new int [cookingStages.Count];
            int index = 0;
            int count = cookingStages.Count;
            while (index < count)
            {
                cookingStagesIdes[index] = cookingStages[index].Id;
                MessageBox.Show(cookingStagesIdes[index].ToString());
                index++;
            }
            ingredientOfStages = new List<IngredientOfStage>(DBConnection.practice.IngredientOfStage.Where(i => cookingStagesIdes.Contains((int)i.CookingStageId)).ToList());
            int[,] ingredientsIdes = new int[ingredientOfStages.Count, ingredientOfStages.Count];
            index = 0;
            count = ingredientOfStages.Count;
            while (index < count)
            {
                ingredientsIdes[index, index] = (int)ingredientOfStages[index].IngredientId;
                MessageBox.Show(ingredientsIdes[index].ToString());
                index++;
            }
            ingredients = new List<Ingredient>(DBConnection.practice.Ingredient.Where(i => ingredientsIdes.Contains(i.Id)).ToList());
            units = new List<Unit>(DBConnection.practice.Unit.ToList());

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
            descTb.Text = dish.Description;

            this.DataContext = this;

            //LISTVIEW

        }
    }
}