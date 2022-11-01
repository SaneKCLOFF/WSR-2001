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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private AddProductWindowViewModel _viewModel;
        public AddProductWindow(List<ProductType> productTypes)
        {
            InitializeComponent();
            _viewModel = (AddProductWindowViewModel)DataContext;
            _viewModel.ProductTypes = productTypes;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddProduct();
        }
    }
}
