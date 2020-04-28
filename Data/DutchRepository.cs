using System;
using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;

        public DutchRepository(DutchContext ctx)
        {
            this._ctx = ctx;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
               return this.GetAllOrders();
            }

            return _ctx.Orders.ToList();
        }

        public Order GetOrderById(string userName, int id)
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(o => o.Product)
                .Where(order => order.Id == id && order.User.UserName == userName)
                .FirstOrDefault();
        }

        public IEnumerable<Order> GetAllOrdersByUser(string userName, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(o => o.Product)
                .Where(order => order.User.UserName == userName)
                .ToList();
            }

            return _ctx.Orders.ToList();
        }

        public void AddOrder(Order newOrder)
        {
            //convert new products to lookup of product
            foreach(var item in newOrder.Items)
            {
                item.Product = _ctx.Products.Find(item.Product.Id);
            }

            AddEntity(newOrder);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products
                .OrderBy(o => o.Title)
                .ToList();
        }

      
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public bool SaveAll()
        {
           return _ctx.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

      
    }
}
