using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для addEdit.xaml
    /// </summary>
    public partial class addEdit : Window
    {
        public addEdit(Book author)
        {
            InitializeComponent();
            DataContext = author ?? new Book("", "", 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Title.Text))
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Введите корректное название");
            }
        }
    }
}
