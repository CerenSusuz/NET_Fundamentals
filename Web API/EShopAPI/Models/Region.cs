using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopAPI.Models;

[Table("Region")]
public partial class Region
{
    [Key]
    public int RegionID { get; set; }

    [Required]
    [StringLength(50)]
    public string RegionDescription { get; set; }

    [InverseProperty("Region")]
    public virtual ICollection<Territory> Territories { get; set; } = new List<Territory>();
}