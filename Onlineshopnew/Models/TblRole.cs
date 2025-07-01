using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Onlineshopnew.Models;

[Table("TblRole")]
public partial class TblRole
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(32)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(32)]
    public string? Title { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
