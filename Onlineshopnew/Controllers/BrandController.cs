using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onlineshopnew.Dto.Brand;
using Onlineshopnew.Dto.Product;
using Onlineshopnew.Models;

namespace Onlineshopnew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ContextDB _context;

        public BrandController(ContextDB context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBrandDto>> Get(int id)
        {
            var brand = await _context.TblBrands.FindAsync(id);
            if (brand == null)
                return NotFound();

            return Ok(new GetBrandDto(brand));
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] EditBrandDto brandDto)
        {
            if (id != brandDto.id)
                return BadRequest("شناسه ارسالی با مسیر متفاوت است");

            var brand = await _context.TblBrands.FindAsync(id);
            if (brand == null)
                return NotFound("برند یافت نشد");

            brand.Name = brandDto.Name;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<TblBrand>> Add([FromBody] AddBrandDto Brand)
        {
            try
            {
                if (Brand == null)
                    return BadRequest("محصول ارسال نشده");

                if (string.IsNullOrWhiteSpace(Brand.Name))
                    return BadRequest("نام محصول الزامی است");

                var b = Brand.ToTBL();


                await _context.TblBrands.AddAsync(b);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = b.Id }, new GetBrandDto(b));
            }
            catch (Exception)
            {
                return StatusCode(500, "خطایی رخ داد، دوباره تلاش کنید.");
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<GetBrandDto>>> Get()
        {
            var brands = await _context.TblBrands.ToListAsync();
            if (brands.Count == 0) return NoContent();

            var dto = new List<GetBrandDto>();
            brands.ForEach(i => dto.Add(new GetBrandDto(i)));
            return Ok(dto);
        }
    
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            TblBrand b = await _context.TblBrands.FindAsync(id);
            if (b == null) return NotFound();

            _context.TblBrands.Remove(b);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
