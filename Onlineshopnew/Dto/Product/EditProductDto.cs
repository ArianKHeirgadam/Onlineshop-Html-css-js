using Onlineshopnew.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Onlineshopnew.Dto.Product
{
    public class EditProductDto : AddProductDto
    {
        public int Id { get; set; }

        public override TblProduct ToTbl()
        {
            return new TblProduct
            {
                Id = Id,
                Name = Name,
                BrandId = BrandId,
                Price = Price,
                Description = Description,
                Image = Image
            };
        }
    }
}
