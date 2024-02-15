using AdoNetFundamentals.Entities;
using System.Data.SqlClient;

namespace AdoNetFundamentals.DLL.Repositories;

public class ProductRepository : IProductRepository
{
    public SqlCommand GetCommand(string query)
    {
        var connection = new SqlConnection(AdoNetDbContext.ConnectionString);
        var command = new SqlCommand(query, connection);

        connection.Open();

        return command;
    }

    public Product MapProduct(SqlDataReader reader)
    {
        return new Product
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Description = reader.GetString(reader.GetOrdinal("Description")),
            Weight = reader.GetDouble(reader.GetOrdinal("Weight")),
            Height = reader.GetDouble(reader.GetOrdinal("Height")),
            Width = reader.GetDouble(reader.GetOrdinal("Width")),
            Lenght = reader.GetDouble(reader.GetOrdinal("Length")),
        };
    }

    public void CreateProduct(Product product)
    {
        string query = "INSERT INTO Product " +
            "(Name, Description, Weight, Height, Width, Length)" +
            "VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)";

        using (var command = GetCommand(query))
        {
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Weight", product.Weight);
            command.Parameters.AddWithValue("@Height", product.Height);
            command.Parameters.AddWithValue("@Width", product.Width);
            command.Parameters.AddWithValue("@Length", product.Lenght);

            command.ExecuteNonQuery();
        }
    }

    public List<Product> GetAllProducts()
    {
        var products = new List<Product>();
        string query = "SELECT * FROM Product";

        using (var command = GetCommand(query))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var product = MapProduct(reader);
                    products.Add(product);
                }
            }
        }

        return products;
    }

    public Product ReadProduct(int id)
    {
        Product product = null;
        string query = "SELECT * FROM Product WHERE Id = @Id";

        using (var command = GetCommand(query))
        {
            command.Parameters.AddWithValue("@Id", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    product = MapProduct(reader);
                }
            }
        }

        return product;
    }

    public void UpdateProduct(Product product)
    {
        string query = $"UPDATE Product " +
                       $"SET Name = @Name, " +
                       $"Description = @Description, " +
                       $"Weight = @Weight, " +
                       $"Height = @Height, " +
                       $"Width = @Width, " +
                       $"Length = @Length " +
                       $"WHERE Id = @Id";

        using (var command = GetCommand(query))
        {
            command.Parameters.AddWithValue("@Id", product.Id);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Weight", product.Weight);
            command.Parameters.AddWithValue("@Height", product.Height);
            command.Parameters.AddWithValue("@Width", product.Width);
            command.Parameters.AddWithValue("@Length", product.Lenght);

            command.ExecuteNonQuery();
        }
    }

    public void DeleteProduct(int id)
    {
        string query = "DELETE FROM Product WHERE Id = @Id";

        using (var command = GetCommand(query))
        {
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
    }
}
