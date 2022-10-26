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
        public List<Product> Products { get; }
        public MainViewModel()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Products = context.Products
                    .Include(pt=>pt.ProductType)
                    .Include(pm => pm.ProductMaterials)
                    .ThenInclude(m=>m.Material)
                    .ToList();
            }
        }
    }

}
