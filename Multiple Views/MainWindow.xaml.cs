﻿using System;
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
using Multiple_Views.ViewModels;
using Multiple_Views.Views;

namespace Multiple_Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new BackCabModel();
        }

        private BackCabModel BVM = new BackCabModel();

        private void RedView_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new RedViewModel();

        }

        private void BackCab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = BVM;
            
        }

    }
}
