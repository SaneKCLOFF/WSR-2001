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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Axyonov.Lopushok.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainViewModel)DataContext;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow =  new AddProductWindow(_viewModel.ProductTypes);
            addProductWindow.ShowDialog();
        }
    }
}
