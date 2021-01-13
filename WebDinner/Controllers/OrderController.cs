using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDinner.Models;
using WebDinner.Models.ViewModels;

namespace WebDinner.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        Cart cart;
        IOrderRepository orderRepository;
        public OrderController(Cart cart, IOrderRepository orderRepository)
        {
            this.cart = cart;
            this.orderRepository = orderRepository;
        }

        [Authorize]
        public ViewResult List() => View(orderRepository.Orders.Where(o => !o.Snipped));

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = orderRepository.Orders.FirstOrDefault(o => o.Id == orderID);
            if (order != null)
            {
                order.Snipped = true;
                orderRepository.SaveOrder(order, cart);
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                orderRepository.SaveOrder(order, cart);
                return RedirectToAction(nameof(Completed));
            }
            else
                return View(order);
        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }

        public IActionResult ToMainPage()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}