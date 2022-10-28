using Axyonov.Lopushok.Domain.Entities;
using Axyonov.Lopushok.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Axyonov.Lopushok.Presentation.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        #region Fields
        public List<Product> _products = new();
        private List<string> _sorterItemsList = new();
        private List<string> _filtherItemsList = new();
        private string _selectedSorter = null!;
        private string _selectedFilter = null!;
        private string _search = null!;
        public List<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        public List<string> SorterItemsList
        {
            get => _sorterItemsList;
            set => _sorterItemsList = value;
        }
        public List<string> FiltherItemsList
        {
            get => _filtherItemsList;
            set => _filtherItemsList = value;
        }
        public string SelectedSorter
        {
            get => _selectedSorter;
            set
            {
                _selectedSorter = value;
                Products = SortProducts(GetProducts(), value);
                OnPropertyChanged(nameof(SelectedSorter));
            }
        }
        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                Products = FilthProducts(GetProducts(), value);
                OnPropertyChanged(nameof(SelectedFilter));
            }
        }

        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                Products = SearchProducts(GetProducts(), value);
                OnPropertyChanged(nameof(Search));
            }
        }
        #endregion
        public MainViewModel()
        {
            #region Загрузка данных в список
            Products = GetProducts();
            SorterItemsList = GetSorterItems();
            FiltherItemsList = GetFiltherItems();
            SelectedSorter = "Без сортировки";
            SelectedFilter = "Без фильтров";
            #endregion
        }
        #region Сортировка, фильтрация, поиск
        private List<Product> GetProducts()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Products
                     .Include(pt => pt.ProductType)
                     .Include(pm => pm.ProductMaterials)
                     .ThenInclude(m => m.Material)
                     .ToList();
            }
        }
        private List<string> GetSorterItems()
        {
            List<string> Sorters = new();
            Sorters.Add("Без сортировки");
            Sorters.Add("По возрастанию стоимости");
            Sorters.Add("По убыванию стоимости");
            Sorters.Add("По названию (возр.)");
            Sorters.Add("По названию (убыв.)");
            return Sorters;
        }
        private List<string> GetFiltherItems()
        {
            List<string> Filthers = new();
            Filthers.Add("Без фильтров");
            using (ApplicationDbContext context = new())
            {
                foreach (var item in context.ProductTypes)
                {
                    Filthers.Add(item.Title);
                }
            }
            return Filthers;
        }
        private List<Product> SortProducts(List<Product> lProducts, string value)
        {
            List<Product> SortList = new();
            if (value == "Без сортировки")
            {
                SortList = GetProducts();
            }
            else if (value == "По возрастанию стоимости" && (Search != null || Search != string.Empty))
            {
                SortList = lProducts.OrderBy(p => p.TotalCost).ToList();
                SortList=SearchProducts(SortList, Search);
            }
            else if (value == "По возрастанию стоимости")
            {
                SortList = lProducts.OrderBy(p => p.TotalCost).ToList();
            }
            else if (value == "По убыванию стоимости" && (Search != null || Search != string.Empty))
            {
                SortList = lProducts.OrderByDescending(p => p.TotalCost).ToList();
                SortList = SearchProducts(SortList, Search);
            }
            else if (value == "По убыванию стоимости")
            {
                SortList = lProducts.OrderByDescending(p => p.TotalCost).ToList();
            }
            else if (value == "По названию (возр.)" && (Search != null || Search != string.Empty))
            {
                SortList = lProducts.OrderBy(p => p.Title).ToList();
                SortList = SearchProducts(SortList, Search);
            }
            else if (value == "По названию (возр.)")
            {
                SortList = lProducts.OrderBy(p => p.Title).ToList();
            }
            else if (value == "По названию (убыв.)" && (Search != null || Search != string.Empty))
            {
                SortList = lProducts.OrderByDescending(p => p.Title).ToList();
            }
            else if (value == "По названию (убыв.)")
            {
                SortList = lProducts.OrderByDescending(p => p.Title).ToList();
                SortList = SearchProducts(SortList, Search);
            }
            return SortList;
        }

        private List<Product> FilthProducts(List<Product> lProducts, string value)
        {
            List<Product> FilthList = new();
            
            if (value == "Без фильтров" && ((Search != null || Search != string.Empty) && SelectedSorter != "Без сортировки")) //search, sort
            {
                FilthList = SearchProducts(SortProducts(lProducts, SelectedSorter), Search);
            }
            else if (value == "Без фильтров" && ((Search == null || Search == string.Empty) && SelectedSorter != "Без сортировки")) //sort
            {
                FilthList = SortProducts(lProducts, SelectedSorter);
            }
            else if (value == "Без фильтров" && ((Search != null || Search != string.Empty) && SelectedSorter == "Без сортировки")) //search
            {
                FilthList = SearchProducts(lProducts, Search);
            }
            else if (value == "Без фильтров") //no filther
            {
                FilthList = lProducts;
            }
            else if ((Search != null || Search != string.Empty) && SelectedSorter != "Без сортировки") //filther, search, sort
            {
                FilthList = lProducts.Where(t => t.ProductType.Title == value).ToList();
                FilthList = SearchProducts(SortProducts(FilthList,SelectedSorter), Search);
            }
            else if ((Search == null || Search == string.Empty) && SelectedSorter != "Без сортировки") //filther, sort
            {
                FilthList = lProducts.Where(t => t.ProductType.Title == value).ToList();
                FilthList = SortProducts(FilthList, SelectedSorter);
            }
            else if ((Search != null || Search != string.Empty) && SelectedSorter == "Без сортировки") //filther, search
            {
                FilthList = lProducts.Where(t => t.ProductType.Title == value).ToList();
                FilthList = SearchProducts(FilthList, Search);
            }
            else //filther
            {
                FilthList = lProducts.Where(t => t.ProductType.Title == value).ToList();
            }
            return FilthList;
        }
        private List<Product> SearchProducts(List<Product> lProducts, string value)
        {
            List<Product> SearchList = new();
            if (value == string.Empty || value == null)
            {
                SearchList = lProducts;
            }
            else
            {
                SearchList = lProducts.Where(ptitle => ptitle.Title.ToLower().Contains(value.ToLower())).ToList();
            }
            return SearchList;
        }
        #endregion

        #region Commands
        private RelayCommand _openAddShelfWindowCommand;
        public RelayCommand OpenAddShelfWindowCommand
        {
            get
            {
                return _openAddShelfWindowCommand ?? new RelayCommand(obj =>
                {
                    OpenAddShelfWindow();
                });
            }
        }
        private void OpenAddShelfWindow()
        {
            AddShelfWindow addShelfWindow = new AddShelfWindow();
            addShelfWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addShelfWindow.ShowDialog();
        }
        #endregion
    }

}
