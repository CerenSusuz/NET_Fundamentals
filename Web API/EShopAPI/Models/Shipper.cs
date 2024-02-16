using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopAPI.Models;

public partial class Shipper
{
    [Key]
    public int ShipperID { get; set; }

    [Required]
    [StringLength(40)]
    public string CompanyName { get; set; }

    [StringLength(24)]
    public string Phone { get; set; }

    [InverseProperty("ShipViaNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}