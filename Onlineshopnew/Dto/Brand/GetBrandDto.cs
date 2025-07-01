using Onlineshopnew.Models;

namespace Onlineshopnew.Dto.Brand
{
    public class GetBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GetBrandDto(TblBrand b)
        {
            Id = b.Id;
            Name = b.Name;
        }
    }
}
