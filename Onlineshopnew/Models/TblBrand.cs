using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Onlineshopnew.Models;

[Table("TblBrand")]
public partial class TblBrand
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [InverseProperty("Brand")]
    [JsonIgnore]
    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}
