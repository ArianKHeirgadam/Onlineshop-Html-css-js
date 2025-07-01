using Onlineshopnew.Models;

namespace Onlineshopnew.Dto.Product
{
    public class GetProductDto
    {
        public int Id { get; set; }


        public string Name { get; set; } = null!;

        public int Price { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public short Sku { get; set; }


        public string Image { get; set; } = null!;

        public double Rating { get; set; }
        public GetProductDto(TblProduct Product)
        {
            Id = Product.Id;
            Name = Product.Name;
            Price = Product.Price;
            BrandId = Product.BrandId;
            Description = Product.Description;
            Sku = Product.Sku;
            Image = Product.Image;
            Rating = Product.Rating;
            BrandName = Product.Brand?.Name ?? "";
        }

    }
}
