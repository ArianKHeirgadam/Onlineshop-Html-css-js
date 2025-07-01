using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Onlineshopnew.Models;

[Table("TblProduct")]
public partial class TblProduct
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public int BrandId { get; set; }

    public string Description { get; set; } = null!;

    public short Sku { get; set; }

    [StringLength(255)]
    public string Image { get; set; } = null!;

    public double Rating { get; set; }

    public bool IsDisabled { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("TblProducts")]
    public virtual TblBrand Brand { get; set; } = null!;
}
