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
    internal class AddProductViewModel:ViewModelBase
    {
        #region Fields
        public List<ProductType> ProductTypeList { get; }
        public string SelectedTitle { get; set; }
        public ProductType SelectedProductType { get; set; }
        public string SelectedArticleNumber { get; set; }
        public string SelectedDescription { get; set; }
        public string SelectedImage { get; set; }
        public string SelectedProductionPersonCount { get; set; }
        public string SelectedProductionWorkshopNumber { get; set; }
        public string SelectedMinCostForAgent { get; set; }
        #endregion

        private RelayCommand? _addProduct;

        public AddProductViewModel()
        {
            using (ApplicationDbContext context = new())
            {
                ProductTypeList = context.ProductTypes.ToList();
            }
        }

        public RelayCommand AddProduct
        {
            get
            {
                return _addProduct ?? new RelayCommand(obj =>
                {
                    using (ApplicationDbContext context = new())
                    {
                        if (!context.Products.Any(pr => pr.Title == SelectedTitle))
                        {
                            Product newProduct = new()
                            {
                                Title = SelectedTitle,
                                ProductTypeId=SelectedProductType.Id,
                                ArticleNumber=SelectedArticleNumber,
                                Description=SelectedDescription,
                                Image=SelectedImage,
                                ProductionPersonCount=Convert.ToInt32(SelectedProductionPersonCount),
                                ProductionWorkshopNumber=Convert.ToInt32(SelectedProductionWorkshopNumber),
                                MinCostForAgent=Convert.ToDecimal(SelectedMinCostForAgent)

                            };
                            context.Products.Add(newProduct);
                            context.SaveChanges();
                            MessageBox.Show($"Продукт '{SelectedTitle}' добавлен!");
                        }
                        else MessageBox.Show($"Продукт {SelectedTitle} уже существует!");
                    }
                });
            }
        }
    }
}
