using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopDemo.Models;

public partial class Shipper
{
    [Key]
    [Column("ShipperID")]
    public int ShipperId { get; set; }

    [Required]
    [StringLength(40)]
    public string CompanyName { get; set; }

    [StringLength(24)]
    public string Phone { get; set; }

    [InverseProperty("ShipViaNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}