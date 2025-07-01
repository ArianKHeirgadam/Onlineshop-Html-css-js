using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Onlineshopnew.Dto.Product;
using Onlineshopnew.Models;
using System.Security.Claims;

namespace Onlineshopnew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ContextDB _context;
        public ProductController(ContextDB context)
        {
            _context = context;
        }
        [HttpGet]
        
        public async Task<ActionResult> GetProducts()
        {
            List<TblProduct> products = await _context.TblProducts
                .Include(i => i.Brand)
                .IgnoreQueryFilters()
                .ToListAsync();

            List<GetProductDto> dto = new List<GetProductDto>();

            var roleClaim = HttpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Role);
            if (roleClaim == null)
            {
                return Unauthorized("نقش کاربر یافت نشد");
            }

            var role = roleClaim.Value;

            if (role == "Seller" || role == "Admin")
            {
                products.ForEach(i => dto.Add(new GetProductDto(i)));
            }
            else if (role == "Costumer")
            {
                var filteredProducts = products.Where(i => i.Brand.Name == "Apple").ToList();
                filteredProducts.ForEach(i => dto.Add(new GetProductDto(i)));
            }

            return Ok(dto);
        }

     
        [HttpPost]
        public async Task<ActionResult<TblProduct>> Add([FromBody] AddProductDto product)
        {
            try
            {
                if (product == null)
                    return BadRequest("محصول ارسال نشده");

                if (string.IsNullOrWhiteSpace(product.Name))
                    return BadRequest("نام محصول الزامی است");

                if (product.sku < short.MinValue || product.sku > short.MaxValue)
                    return BadRequest("SKU مقدار نامعتبر دارد");

                var p = product.ToTbl();

                //if (string.IsNullOrEmpty(p.Image))
                //    p.Image = "default.jpg";

                await _context.TblProducts.AddAsync(p);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = p.Id },new GetProductDto(p));
            }
            catch (Exception)
            {
                return StatusCode(500, "خطایی رخ داد، دوباره تلاش کنید.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] EditProductDto product)
        {
            var p = await _context.TblProducts.FindAsync(product.Id);
            if (p == null) return NotFound();

            p.Name = product.Name;
            p.Price = product.Price;
            p.BrandId = product.BrandId;
            p.Description = product.Description;
            p.Image = product.Image;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TblProduct>> Get(int id)
        {
            TblProduct p = await _context.TblProducts.Include(i => i.Brand).FirstOrDefaultAsync(i => i.Id == id);
            if (p == null) return NotFound();
            return Ok(new GetProductDto(p));
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            TblProduct p = await _context.TblProducts.FindAsync(id);
            if (p == null) return NotFound();

            _context.TblProducts.Remove(p);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("GetByBrand/{brandId}")]
        public async Task<ActionResult<List<GetProductDto>>> GetByBrand(int brandId)
        {
            var products = await _context.TblProducts.Include(i => i.Brand).Where(i => i.BrandId == brandId).ToListAsync();
            if (products.Count == 0) return NotFound();
            var dto = new List<GetProductDto>();
            products.ForEach(i => dto.Add(new GetProductDto(i)));
            return Ok(dto);
        }
    }
}
    
