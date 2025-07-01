using Onlineshopnew.Models;

namespace Onlineshopnew.Dto.Product
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public short sku { get; set; }
        public virtual TblProduct ToTbl()
        {
            return new TblProduct
            {
                Name = Name,
                BrandId = BrandId,
                Price = Price,
                Description = Description,
                Image = Image,
                Sku = sku
            };
        }
    }

}

