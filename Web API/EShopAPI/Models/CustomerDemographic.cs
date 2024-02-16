using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopAPI.Models;

public partial class CustomerDemographic
{
    [Key]
    [StringLength(10)]
    public string CustomerTypeID { get; set; }

    [Column(TypeName = "ntext")]
    public string CustomerDesc { get; set; }

    [ForeignKey("CustomerTypeID")]
    [InverseProperty("CustomerTypes")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}