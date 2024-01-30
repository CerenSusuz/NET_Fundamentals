using AdoNetFundamentals.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetFundamentals.DLL.Repositories
{
    public interface IProductRepository
    {
        Product ReadProduct(int id);

        void UpdateProduct(Product product);

        void DeleteProduct(int id);

        List<Product> GetAllProducts();
    }
}
