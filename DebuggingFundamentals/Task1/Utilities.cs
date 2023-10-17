using System;

namespace Task1
{
    public static class Utilities
    {
        /// <summary>
        /// Sorts an array in ascending order using bubble sort.
        /// </summary>
        /// <param name="numbers">Numbers to sort.</param>
        public static void Sort(int[] numbers)
        {
            if (numbers == null)
            {
                throw new ArgumentNullException("numbers cannot be null");
            }

            int temp;

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i; j < numbers.Length; j++)
                {
                    if (numbers[i] > numbers[j])
                    {
                        temp = numbers[j];
                        numbers[j] = numbers[i];
                        numbers[i] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Searches for the index of a product in an <paramref name="products"/> 
        /// based on a <paramref name="predicate"/>.
        /// </summary>
        /// <param name="products">Products used for searching.</param>
        /// <param name="predicate">Product predicate.</param>
        /// <returns>If match found then returns index of product in <paramref name="products"/>
        /// otherwise -1.</returns>
        public static int IndexOf(Product[] products, Predicate<Product> predicate)
        {
            if (products == null)
            {
                throw new ArgumentNullException("products cannot be null");
            }

            for (int i = 0; i < products.Length; i++)
            {
                var product = products[i];

                try
                {
                    if (predicate(product))
                    {
                        return i;
                    }
                }
                catch (NullReferenceException ex)
                {
                    throw new ArgumentNullException(ex.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return -1;
        }
    }
}
