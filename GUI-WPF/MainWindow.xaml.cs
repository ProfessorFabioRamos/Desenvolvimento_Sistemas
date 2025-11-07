using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using System.Collections.ObjectModel;
using CadastroProdutos.Models;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>

namespace CadastroProdutos{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> _products;
        public MainWindow()
        {
            InitializeComponent();
            _products = new ObservableCollection<Product>();
            dgProducts.ItemsSource = _products;
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e){
            float price = float.TryParse(txtPrice.Text, out var p) ? p : 0;
            int qty = int.TryParse(txtQuantity.Text, out var q) ? q : 0;

            if(string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCategory.Text)){
                MessageBox.Show("Preencha todos os campos!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _products.Add(
                new Product {
                    Name = txtName.Text,
                    Category = txtCategory.Text, 
                    Price = price, 
                    Quantity = qty
                }
            );

            txtName.Clear();
            txtCategory.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
        }

        private void Remover_Click(object sender, RoutedEventArgs e){
            if(dgProducts.SelectedItem is Product productSelected){
                _products.Remove(productSelected);
            } else {
                MessageBox.Show("Selecione um produto!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
