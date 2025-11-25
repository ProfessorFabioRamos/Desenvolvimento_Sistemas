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
using CadastroProdutos.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>

namespace CadastroProdutos{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public MainWindow()
        {
            InitializeComponent();
            LoadProductsFromDatabase();
            dgProducts.ItemsSource = _products;
        }

        private void LoadProductsFromDatabase(){
            using (var context = new AppDbContext()){
                context.Database.EnsureCreated();
                var productsFromDb = context.Products.ToList();
                _products = new ObservableCollection<Product>(productsFromDb);
            }
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

        private void SalvarBanco_Click(object sender, RoutedEventArgs e){
            using (var context = new AppDbContext()){
                // Cria o banco se ele não existir
                context.Database.EnsureCreated();
                //Adiciona todos os produtos da lista ao context(banco)
                context.Products.AddRange(_products);
                //Salva o banco no produtos.db
                context.SaveChanges();
                // Caixa de mensagem em caso de sucesso
                MessageBox.Show("Dados salvos com sucesso!", "Sucesso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
