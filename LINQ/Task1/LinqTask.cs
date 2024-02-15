using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1;

public static class LinqTask
{
    public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
    {
        return customers.Where(customer => customer.Orders.Sum(order => order.Total) > limit);
    }

    public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
        IEnumerable<Customer> customers,
        IEnumerable<Supplier> suppliers
    )
    {
        return customers.Select(customer => (customer: customer, suppliers: suppliers
                        .Where(supplier => supplier.Country == customer.Country && supplier.City == customer.City)))
                        .ToList();
    }

    public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
        IEnumerable<Customer> customers,
        IEnumerable<Supplier> suppliers
    )
    {
        return from customer in customers
               join supplier in suppliers on new { customer.City, customer.Country } equals new { supplier.City, supplier.Country } into customersupplier
               select (customer, customersupplier);
    }

    public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
    {
        return from customer in customers
               where customer.Orders.Any(order => order.Total > limit)
               select customer;
    }

    public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
        IEnumerable<Customer> customers
    )
    {
        return customers.Where(customer => customer.Orders.Any()).Select(customer => (customer: customer, dateOfEntry: customer.Orders.Min(order => order.OrderDate)));
    }

    public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
        IEnumerable<Customer> customers
    )
    {
        var customersWithDateOfEntry = Linq4(customers);

        return customersWithDateOfEntry.OrderBy(customer => customer.dateOfEntry.Year)
                                       .ThenBy(date => date.dateOfEntry.Month)
                                       .ThenByDescending(customer => customer.customer.Orders
                                       .Sum(order => order.Total))
                                       .ThenBy(company => company.customer.CompanyName);
    }

    public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
    {
        return customers.Where(customer => customer.PostalCode == null || !customer.PostalCode.All(char.IsDigit) || customer.Region == null || !customer.Phone.Contains(')'));
    }

    public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
    {
        /* example of Linq7result

         category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
         */

        return products.GroupBy(product => product.Category)
            .Select(categoryGroup => new Linq7CategoryGroup
            {
                Category = categoryGroup.Key,
                UnitsInStockGroup = categoryGroup.GroupBy(product => product.UnitsInStock)
                .Select(unitsInStockGroup => new Linq7UnitsInStockGroup
                {
                    UnitsInStock = unitsInStockGroup.Key,
                    Prices = unitsInStockGroup.Select(product => product.UnitPrice)
                })
            });
    }

    public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
        IEnumerable<Product> products,
        decimal cheap,
        decimal middle,
        decimal expensive
    )
    {
        return new[]
        {
            (priceRange: cheap, products: products.Where(product => product.UnitPrice <= cheap)),
            (priceRange: middle, products: products.Where(product => product.UnitPrice > cheap && product.UnitPrice <= middle)),
            (priceRange: expensive, products: products.Where(product => product.UnitPrice > middle && product.UnitPrice <= expensive))
        };
    }

    public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
        IEnumerable<Customer> customers
    )
    {
        return customers.GroupBy(customer => customer.City)
            .Select(group => (cityName: group.Key,
            averageIncome: (int)Math.Round(group.Sum(customer => customer.Orders.Sum(order => order.Total)) / group.Count()),
            averageIntensity: group.Sum(customer => customer.Orders.Length) / group.Count()));
    }

    public static string Linq10(IEnumerable<Supplier> suppliers)
    {
        return string.Concat(suppliers.Select(supplier => supplier.Country)
            .Distinct()
            .OrderBy(country => country.Length)
            .ThenBy(country => country));
    }
}