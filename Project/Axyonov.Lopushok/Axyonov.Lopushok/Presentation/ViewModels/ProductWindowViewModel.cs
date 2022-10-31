using Axyonov.Lopushok.Domain.Entities;
using Axyonov.Lopushok.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axyonov.Lopushok.Presentation.ViewModels
{
    internal class ProductWindowViewModel : ViewModelBase
    {
        private Product _selectedProduct=null!;
        private List<Product> _products = null!;
        private List<ProductType> _productTypes = null!;
        private List<ProductMaterial> _productMaterials = null!;
        private ProductType _selectedProductType = null!;

        public Product SelectedProduct 
        { 
            get => _selectedProduct;
            set 
            {
                Set(ref _selectedProduct, value, nameof(SelectedProduct));
                ProductMaterials = SelectedProduct.ProductMaterials.ToList();
            } 
        }
        public List<Product> Products 
        { 
            get => _products;
            set => Set(ref _products, value, nameof(Products));
        }
        public List<ProductType> ProductTypes 
        { 
            get => _productTypes;
            set => Set(ref _productTypes, value, nameof(ProductTypes));
        }
        public List<ProductMaterial> ProductMaterials 
        { 
            get => _productMaterials; 
            set => Set(ref _productMaterials, value, nameof(ProductMaterials));
        }
        public ProductType SelectedProductType 
        { 
            get => _selectedProductType;
            set => Set(ref _selectedProductType, value, nameof(SelectedProductType));
        }
        public void SaveChanges()
        {
            using (ApplicationDbContext context = new())
            {
                context.Products.Update(SelectedProduct);
                SelectedProduct.ProductTypeId = SelectedProductType.Id;
                context.SaveChanges();
            }
        }
    } 
}
