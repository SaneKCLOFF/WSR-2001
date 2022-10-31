using Axyonov.Lopushok.Domain.Entities;
using Axyonov.Lopushok.Presentation.ViewModels;
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
using System.Windows.Shapes;

namespace Axyonov.Lopushok.Presentation
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private ProductWindowViewModel _viewModel;
        public ProductWindow(Product selectedProduct,List<Product> products,List<ProductType> productTypes)
        {
            InitializeComponent();
            _viewModel = (ProductWindowViewModel)DataContext;
            _viewModel.SelectedProduct = selectedProduct;
            _viewModel.Products = products;
            _viewModel.ProductTypes = productTypes;
            _viewModel.SelectedProductType = selectedProduct.ProductType; ;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveChanges();
            Close();
        }
    }
}
