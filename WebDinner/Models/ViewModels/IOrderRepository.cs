using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDinner.Models.ViewModels
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; set; }
        void SaveOrder(Order order, Cart cart);
    }

    public class OrderRepository : IOrderRepository
    {
        private ProductsDBContext context;

        public OrderRepository(ProductsDBContext context)
        {
            this.context = context;
            Orders = context.Order;
        }

        public IQueryable<Order> Orders { get; set; }

        public void SaveOrder(Order order, Cart cart)
        {
            string clientOrder = "";

            if (cart.FirstDishes.Count() > 0)
            {
                foreach (var t in cart.FirstDishes)
                    clientOrder += t.Meal.Name + ",";
            }
            if (cart.SecondDishes.Count() > 0)
            {
                foreach (var t in cart.SecondDishes)
                    clientOrder += t.Meal.Name + ",";
            }
            if(cart.Salads.Count() > 0)
            {
                foreach (var t in cart.Salads)
                    clientOrder += t.Meal.Name + ",";
            }
            
            context.Order.Add(new Order()
            {
                Name = order.Name,
                Address = order.Address,
                OrderDate = DateTime.Now,
                OrderDescription = clientOrder,
                Snipped = order.Snipped == true ? order.Snipped : false
            });
            context.SaveChanges();
            Orders.Append(order);
        }
    }
}
