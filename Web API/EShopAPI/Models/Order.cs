﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EShopAPI.Models;

[Index("CustomerID", Name = "CustomerID")]
[Index("CustomerID", Name = "CustomersOrders")]
[Index("EmployeeID", Name = "EmployeeID")]
[Index("EmployeeID", Name = "EmployeesOrders")]
[Index("OrderDate", Name = "OrderDate")]
[Index("ShipPostalCode", Name = "ShipPostalCode")]
[Index("ShippedDate", Name = "ShippedDate")]
[Index("ShipVia", Name = "ShippersOrders")]
public partial class Order
{
    [Key]
    public int OrderID { get; set; }

    [StringLength(5)]
    public string CustomerID { get; set; }

    public int? EmployeeID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OrderDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RequiredDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ShippedDate { get; set; }

    public int? ShipVia { get; set; }

    [Column(TypeName = "money")]
    public decimal? Freight { get; set; }

    [StringLength(40)]
    public string ShipName { get; set; }

    [StringLength(60)]
    public string ShipAddress { get; set; }

    [StringLength(15)]
    public string ShipCity { get; set; }

    [StringLength(15)]
    public string ShipRegion { get; set; }

    [StringLength(10)]
    public string ShipPostalCode { get; set; }

    [StringLength(15)]
    public string ShipCountry { get; set; }

    [ForeignKey("CustomerID")]
    [InverseProperty("Orders")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("EmployeeID")]
    [InverseProperty("Orders")]
    public virtual Employee Employee { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<Order_Detail> Order_Details { get; set; } = new List<Order_Detail>();

    [ForeignKey("ShipVia")]
    [InverseProperty("Orders")]
    public virtual Shipper ShipViaNavigation { get; set; }
}