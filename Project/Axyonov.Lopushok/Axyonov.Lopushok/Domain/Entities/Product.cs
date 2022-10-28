using Axyonov.Lopushok.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Axyonov.Lopushok.Domain.Entities
{
    public partial class Product
    {
        public Product()
        {
            ProductCostHistories = new HashSet<ProductCostHistory>();
            ProductMaterials = new HashSet<ProductMaterial>();
            ProductSales = new HashSet<ProductSale>();
        }
        private string? _image;
        private decimal _minCostForAgent;
        public int Id { get; set; }
        public string Title { get; set; }
        public string FullTitle
        {
            get { return $"{ProductType.Title} | {Title}"; }
        }
        public int? ProductTypeId { get; set; }
        public string ArticleNumber { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image
        {
            get => (_image == string.Empty) || (_image == null)
                ? $"..\\Resources\\picture.png"
                : $"..\\Resources{_image.Replace("jpg", "jpeg")}";
            set => _image = value;
        }
        public int? ProductionPersonCount { get; set; }
        public int? ProductionWorkshopNumber { get; set; }
        public decimal MinCostForAgent
        {
            get
            {
                return _minCostForAgent;
            }
            set
            {
                _minCostForAgent = value;
            }
        }

        public virtual ProductType? ProductType { get; set; }
        public virtual ICollection<ProductCostHistory> ProductCostHistories { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<ProductSale> ProductSales { get; set; }
        [NotMapped]
        public string FullMaterials
        {
            get
            {
                if (ProductMaterials.Count()==0)
                {
                    return "Отсутсвуют";
                }
                StringBuilder materialsString = new();
                foreach (var material in ProductMaterials)
                {
                    materialsString.Append($"{material.Material.Title}, ");
                }
                materialsString.Remove(materialsString.Length-2,2);
                return materialsString.ToString();
            }
        }
        [NotMapped]
        public decimal TotalCost
        {
            get
            {
                if (ProductMaterials.Count()==0)
                {
                    return MinCostForAgent;
                };
                var totalCost = 0M;
                foreach (var pm in ProductMaterials)
                {
                    totalCost += Math.Ceiling((decimal)pm.Count) * pm.Material.Cost;
                }
                return totalCost;
            }
        }
    }
}
