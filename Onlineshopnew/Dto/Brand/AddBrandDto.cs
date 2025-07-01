using Onlineshopnew.Models; 

namespace Onlineshopnew.Dto.Brand
{
    public class AddBrandDto
    {
        public string Name { get; set; }
        public virtual TblBrand ToTBL()
        {
            return new TblBrand
            {
                Name = Name,
            };
        }
    }
}
