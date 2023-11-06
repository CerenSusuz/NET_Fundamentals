using System;

namespace Task1
{
    public class Product
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public double Price { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Product)
            {
                var product = obj as Product;

                return product.Name == Name && product.Price == Price;
            }

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Name, Price);
    }
}
