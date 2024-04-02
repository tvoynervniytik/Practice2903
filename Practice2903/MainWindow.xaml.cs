﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Practice2903
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// #d5c847 #fdb249 #fed7c1 #eed4fe
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frNav.Navigate(new Pages.MainPage());
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            frNav.NavigationService.Navigate(new Pages.ListOfDishes());
        }
        private void Hyperlink_Click_2(object sender, RoutedEventArgs e)
        {
            frNav.NavigationService.Navigate(new Pages.ListOfIngredients());
        }
        private void Hyperlink_Click_3(object sender, RoutedEventArgs e)
        {
            frNav.NavigationService.Navigate(new Pages.ListOfOrders());
        }
    }
}
