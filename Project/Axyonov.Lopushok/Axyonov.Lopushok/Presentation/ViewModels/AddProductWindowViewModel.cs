using Axyonov.Lopushok.Domain.Entities;
using Axyonov.Lopushok.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Axyonov.Lopushok.Presentation.ViewModels
{
    internal class AddProductWindowViewModel : ViewModelBase
    {
        //private Product _newProduct = null!;
        //public Product NewProduct
        //{
        //    get => _newProduct;
        //    set => Set(ref _newProduct, value, nameof(NewProduct));
        //}

        private string _addTitle = null!;
        private string _addArticle = null!;
        private string _addDescription = null!;
        private string _addImage = null!;
        private int _addProductionPersonCount;
        private string _addProductionWorkshopNumber = null!;
        private string _addMinCostForAgent = null!;
        private List<ProductType> _productTypes;

        public ProductType SelectedProductType { get; set; } = null!;
        public string AddTitle
        {
            get => _addTitle;
            set => Set(ref _addTitle, value, nameof(AddTitle));
        }
        public string AddArticle { get => _addArticle; set => Set(ref _addArticle, value, nameof(AddArticle)); }
        public string AddDescription { get => _addDescription; set => Set(ref _addDescription, value, nameof(AddDescription)); }
        public string AddImage { get => _addImage; set => Set(ref _addImage, value, nameof(AddImage)); }
        public int AddProductionPersonCount { get => _addProductionPersonCount; set =>Set(ref _addProductionPersonCount, value, nameof(AddProductionPersonCount)); }
        public string AddProductionWorkshopNumber { get => _addProductionWorkshopNumber; set =>Set(ref _addProductionWorkshopNumber, value, nameof(AddProductionWorkshopNumber)); }
        public string AddMinCostForAgent { get => _addMinCostForAgent; set => Set(ref _addMinCostForAgent, value, AddMinCostForAgent); }
        public List<ProductType> ProductTypes { get => _productTypes; set => Set(ref _productTypes, value, nameof(ProductTypes)); }

        public void AddProduct()
        {
            if (AddTitle==null || AddTitle== string.Empty)
            {
                MessageBox.Show("Введите название!");
            }
            else if (SelectedProductType==null)
            {
                MessageBox.Show("Выберите тип!");
            }
            else if (AddArticle==null || AddArticle==string.Empty)
            {
                MessageBox.Show("Введите артикуль!");
            }
            else
            {
                using (ApplicationDbContext context = new())
                {
                    Product newProduct = new Product()
                    {
                        Title = AddTitle,
                        ProductTypeId = SelectedProductType.Id,
                        ArticleNumber = AddArticle,
                        Description = AddDescription,
                        Image = AddImage,
                        ProductionPersonCount = AddProductionPersonCount,
                        ProductionWorkshopNumber = Convert.ToInt32(AddProductionWorkshopNumber),
                        MinCostForAgent = Convert.ToDecimal(AddMinCostForAgent)

                    };
                    context.Products.Add(newProduct);
                    context.SaveChanges();
                    MessageBox.Show("Успешно!");
                }
            }
        }
    }
}
