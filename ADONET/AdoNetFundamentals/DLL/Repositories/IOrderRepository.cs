using System;
using System.Collections.Generic;
using AdoNetFundamentals.Entities;

public interface IOrderRepository
{
    Order Create(Order order);

    Order Read(int id);

    void Update(Order order);

    void Delete(int id);

    void BulkDeleteOrders(string status = null, int month = 0, int year = 0, int productId = 0);

    List<Order> FetchOrdersByFilter(string status = null, int month = 0, int year = 0, int productId = 0);
}