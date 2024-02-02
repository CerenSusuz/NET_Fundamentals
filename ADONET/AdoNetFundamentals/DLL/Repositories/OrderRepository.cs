using AdoNetFundamentals.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AdoNetFundamentals.DLL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public SqlCommand GetCommand(string query)
        {
            var connection = new SqlConnection(AdoNetDbContext.ConnectionString);
            var command = new SqlCommand(query, connection);

            connection.Open();

            return command;
        }

        public Order MapOrder(SqlDataReader reader)
        {
            return new Order
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
            };
        }

        public void BulkDeleteOrders(string status = null, int month = 0, int year = 0, int productId = 0)
        {
            var ordersToDelete = FetchOrdersByFilter(status, month, year, productId);

            if (ordersToDelete.Count > 0)
            {
                using (var connection = new SqlConnection(AdoNetDbContext.ConnectionString))
                {
                    foreach (var order in ordersToDelete)
                    {
                        string deleteQuery = "DELETE FROM Orders WHERE Id = @Id";
                        connection.Open();
                        using (var command = new SqlCommand(deleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Id", order.Id);
                            command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                }
            }
        }

        public Order Create(Order order)
        {
            string query = "INSERT INTO Orders " +
                "(Status, CreatedDate, UpdatedDate, ProductId)" +
                "VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductId)";

            using (var command = GetCommand(query))
            {
                command.Parameters.AddWithValue("@Status", order.Status);
                command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
                command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
                command.Parameters.AddWithValue("@ProductId", order.ProductId);

                command.ExecuteNonQuery();
            }

            return order;
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Orders WHERE Id = @Id";

            using (var command = GetCommand(query))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public List<Order> FetchOrdersByFilter(string status = null, int month = 0, int year = 0, int productId = 0)
        {
            var orders = new List<Order>();
            string query = "SELECT * FROM Orders";

            if (!string.IsNullOrEmpty(status) || month > 0 || year > 0 || productId > 0)
            {
                query += " WHERE";
                List<string> conditions = new List<string>();

                if (!string.IsNullOrEmpty(status)) conditions.Add(" Status = @Status");
                if (month > 0) conditions.Add(" MONTH(CreatedDate) = @Month");
                if (year > 0) conditions.Add(" YEAR(CreatedDate) = @Year");
                if (productId > 0) conditions.Add(" ProductId = @ProductId");

                query += string.Join(" AND", conditions);
            }

            using (var command = GetCommand(query))
            {
                if (!string.IsNullOrEmpty(status)) command.Parameters.AddWithValue("@Status", status);
                if (month > 0) command.Parameters.AddWithValue("@Month", month);
                if (year > 0) command.Parameters.AddWithValue("@Year", year);
                if (productId > 0) command.Parameters.AddWithValue("@ProductId", productId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = MapOrder(reader);
                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        public Order Read(int id)
        {
            Order order = null;
            string query = "SELECT * FROM Orders WHERE Id = @Id";

            using (var command = GetCommand(query))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        order = MapOrder(reader);
                    }
                }
            }

            return order;
        }

        public void Update(Order order)
        {
            string query = "UPDATE Orders " +
                           "SET Status = @Status, " +
                           "CreatedDate = @CreatedDate, " +
                           "UpdatedDate = @UpdatedDate, " +
                           "ProductId = @ProductId " +
                           "WHERE Id = @Id";

            using (var command = GetCommand(query))
            {
                command.Parameters.AddWithValue("@Id", order.Id);
                command.Parameters.AddWithValue("@Status", order.Status);
                command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
                command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
                command.Parameters.AddWithValue("@ProductId", order.ProductId);

                command.ExecuteNonQuery();
            }
        }
    }
}