using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopDemo.Models;

public partial class Territory
{
    [Key]
    [Column("TerritoryID")]
    [StringLength(20)]
    public string TerritoryId { get; set; }

    [Required]
    [StringLength(50)]
    public string TerritoryDescription { get; set; }

    [Column("RegionID")]
    public int RegionId { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("Territories")]
    public virtual Region Region { get; set; }

    [ForeignKey("TerritoryId")]
    [InverseProperty("Territories")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}