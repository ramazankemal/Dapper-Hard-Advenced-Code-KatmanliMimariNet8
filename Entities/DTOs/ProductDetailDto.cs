using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDetailDto:IDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid CateogryId { get; set; }
        public string CategoryName { get; set; }
        public Guid BrandId { get; set; }
        public string WebId { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; }
        public List<ProductImage> Images { get; set; }
        public bool Recommended { get; set; }
    }
}
