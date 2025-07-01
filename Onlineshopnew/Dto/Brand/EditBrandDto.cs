using Onlineshopnew.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Onlineshopnew.Dto.Brand
{
    public class EditBrandDto : AddBrandDto
    {
       public int id { get; set; }
        public TblBrand ToTBL()
        {
            return new TblBrand
            {
                Id = id,
                Name = Name,
            };
        }

    }
}
