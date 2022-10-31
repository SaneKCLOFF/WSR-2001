using Axyonov.Lopushok.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axyonov.Lopushok.Presentation.ViewModels;
using Axyonov.Lopushok.Domain.Entities;
using System.Windows.Documents;

namespace Axyonov.Lopushok.Presentation.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        #region fields
        private List<Product> _products;
        private List<Product> _displayingProducts;
        private string _searchValue;
        private string _sortValue;
        private string _filterValue;
        private List<ProductType> _productTypes;
        private List<string> _valuesToFilther;
        #endregion
        #region properties
        public List<ProductType> ProductTypes
        {
            get => _productTypes;
            set => Set(ref _productTypes, value, nameof(ProductTypes));
        }
        public string FilterValue
        {
            get => _filterValue;
            set
            {
                Set(ref _filterValue, value, nameof(FilterValue));
                DisplayProducts();
            }
        }
        public string SortValue
        {
            get => _sortValue;
            set
            {
                Set(ref _sortValue, value, nameof(SortValue));
                DisplayProducts();
            }
        }
        public string SearchValue
        {
            get => _searchValue;
            set
            {
                Set(ref _searchValue, value, nameof(SearchValue));
                DisplayProducts();
            }
        }
        public List<string> ValuesToFilther
        {
            get => _valuesToFilther;
            set => Set(ref _valuesToFilther, value, nameof(ValuesToFilther));       
        }
        public List<string> ValuesToSort => new List<string>
        {
            "Без сортировки", "По названию(возр.)", "По названию(убыв.)","По стоимости(возр.)","По стоимости(убыв.)"
        };
        public List<Product> DisplayingProducts
        {
            get => _displayingProducts;
            set
            {
                Set(ref _displayingProducts, value, nameof(DisplayingProducts));
            }
        }
        #endregion
        public MainViewModel()
        {
            ValuesToFilther = new List<string>();
            ValuesToFilther.Add("Без фильтра");

            FilterValue = ValuesToFilther[0];
            SortValue = ValuesToSort[0];


            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ProductTypes = context.ProductTypes.ToList();

                ProductTypes.ForEach(pt => ValuesToFilther.Add(pt.Title));

                _products = context.Products
                    .Include(pt => pt.ProductType)
                    .Include(pm => pm.ProductMaterials)
                    .ThenInclude(m => m.Material)
                    .ToList();
            }
            _displayingProducts=new List<Product>(_products);
        }
        #region Sort, Filter, Search
        public void DisplayProducts()
        {
            DisplayingProducts = Sort(Search(Filter(_products)));
        }
        private List<Product> Sort(List<Product> products)
        {
            //"Без сортировки", "По названию(возр.)", "По названию(убыв.)","По стоимости(возр.)","По стоимости(убыв.)"
            if (SortValue == ValuesToSort[1])
                return products.OrderBy(p => p.Title).ToList();
            else if (SortValue == ValuesToSort[2])
                return products.OrderByDescending(p => p.Title).ToList();
            else if (SortValue == ValuesToSort[3])
                return products.OrderBy(p => p.TotalCost).ToList();
            else if (SortValue == ValuesToSort[4])
                return products.OrderByDescending(p => p.TotalCost).ToList();
            else
                return products;
        }
        private List<Product> Filter(List<Product> products)
        {
            if (FilterValue == ValuesToFilther[0])
                return products;
            else
                return products.Where(p => p.ProductType.Title == FilterValue).ToList();
        }
        private List<Product> Search(List<Product> products)
        {
            if (SearchValue==string.Empty||SearchValue==null!)
            {
                return products;
            }
            return products.Where(p => p.Fulltitle.ToLower().Contains(SearchValue.ToLower())).ToList();
        }
        #endregion
    }

}
