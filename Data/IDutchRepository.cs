using System;
using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public IEnumerable<Product> GetProductsByCategory(string category);
        public bool SaveAll();
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
    }
}
