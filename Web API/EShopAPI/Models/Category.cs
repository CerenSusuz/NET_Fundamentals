﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EShopAPI.Models;

[Index("CategoryName", Name = "CategoryName")]
public partial class Category
{
    [Key]
    public int CategoryID { get; set; }

    [Required]
    [StringLength(15)]
    public string CategoryName { get; set; }

    [Column(TypeName = "ntext")]
    public string Description { get; set; }

    [Column(TypeName = "image")]
    public byte[] Picture { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}