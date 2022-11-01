using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Axyonov.Lopushok.Domain.Entities
{
    public partial class Material
    {
        private string? _image;

        public Material()
        {
            MaterialCountHistories = new HashSet<MaterialCountHistory>();
            ProductMaterials = new HashSet<ProductMaterial>();
            Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int CountInPack { get; set; }
        public string Unit { get; set; } = null!;
        public double? CountInStock { get; set; }
        public double MinCount { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }
        public string? Image
        {
            get => (_image == string.Empty || _image == null) ? @"\Resources\picture.png" : @$"\Resources{_image}";
            set => _image = value;
        }
        public int MaterialTypeId { get; set; }

        public virtual MaterialType MaterialType { get; set; } = null!;
        public virtual ICollection<MaterialCountHistory> MaterialCountHistories { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
        [NotMapped]
        public string FullTitle
        {
            get
            {
                return $"{MaterialType.Title} | {Title}";
            }
        }
    }
}
