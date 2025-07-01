using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Onlineshopnew.Models;

[Table("TblUser")]
[Index("Username", Name = "UQ__TblUser__536C85E42E4DFA2E", IsUnique = true)]
public partial class TblUser
{

    [Column("ID")]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(16)]
    public string? Tell { get; set; }

    [Key]
    [StringLength(64)]
    public string Username { get; set; } = null!;

    [StringLength(64)]
    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("TblUsers")]
    public virtual TblRole Role { get; set; } = null!;
}
