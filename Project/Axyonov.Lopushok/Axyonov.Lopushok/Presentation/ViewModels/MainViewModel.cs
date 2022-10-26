using Axyonov.Lopushok.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axyonov.Lopushok.Presentation.ViewModels;
using Axyonov.Lopushok.Domain.Entities;

namespace Axyonov.Lopushok.Presentation.ViewModels
{
    internal class MainViewModel:ViewModelBase
    {
        public List<Product> _products = new();
        private List<string> _sorterItemsList = new();
        private List<string> _filtherItemsList = new();
        private string _selectedSorter=null!;
        private string _selectedFilter=null!;
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
                SortProducts(value);
                OnPropertyChanged(nameof(SelectedSorter));
            } 
        }
        public string SelectedFilter 
        { 
            get => _selectedFilter;
            set 
            {
                _selectedFilter = value;
                FilthProducts(value);
                OnPropertyChanged(nameof(SelectedFilter));
            }
        }

        public MainViewModel()
        {
            #region Загрузка данных в список
            Products = GetProducts();
            SorterItemsList = GetSorterItems();
            FiltherItemsList = GetFiltherItems();
            SelectedSorter ="Без сортировки";
            SelectedFilter = "Без фильтров";
            #endregion
        }
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
            Sorters.Add("По возрастанию");
            Sorters.Add("По убыванию");
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
        private void SortProducts(string value)
        {
            if (value=="Без сортировки")
            {
                Products = GetProducts();
            }
            else if (value=="По возрастанию")
            {
                Products = GetProducts().OrderBy(p => p.Title).ToList();
            }
            else if (value=="По убыванию")
            {
                Products = GetProducts().OrderByDescending(p => p.Title).ToList();
            }
        }

        private void FilthProducts(string value)
        {
            if (value=="Без фильтров")
            {
                Products = GetProducts();
            }
            else
            {
                Products = GetProducts().Where(t => t.ProductType.Title == value).ToList();
            }
        }
    }

}
