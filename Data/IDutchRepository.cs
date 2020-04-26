using System;
using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
       
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string userName, bool includeItems);
        Order GetOrderById(string userName, int id);
      
        void AddEntity(object model);
        bool SaveAll();
        
    }
}
