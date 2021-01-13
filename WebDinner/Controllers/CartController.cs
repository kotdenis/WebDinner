using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDinner.Infrastructure;
using WebDinner.Models;
using WebDinner.Models.ViewModels;

namespace WebDinner.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        IFoodRepository foodRepository;
        IMealRepository mealRepository;
        Cart cart;

        public CartController(IFoodRepository foodRepository, IMealRepository mealRepository, Cart cart)
        {
            this.foodRepository = foodRepository;
            this.mealRepository = mealRepository;
            this.cart = cart;
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                //Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToFirstCart(int Id, string returnUrl)
        {
            FirstDishes firstDishes = mealRepository.FirstDishes
                .FirstOrDefault(x => x.Id == Id);
            if (firstDishes != null)
                cart.AddItem(firstDishes, 1);
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult AddToSecondCart(int Id, string returnUrl)
        {
            SecondDishes secondDishes = mealRepository.SecondDishes
                .FirstOrDefault(x => x.Id == Id);
            if (secondDishes != null)
                cart.AddItem(secondDishes, 1);
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult AddToSaladCart(int Id, string returnUrl)
        {
            Salads salads = mealRepository.Salads
                .FirstOrDefault(x => x.Id == Id);
            if (salads != null)
                cart.AddItem(salads, 1);
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromFirstCart(int Id, string returnUrl)
        {
            FirstDishes firstDishes = mealRepository.FirstDishes
            .FirstOrDefault(p => p.Id == Id);
            if (firstDishes != null)
            {
                cart.RemoveLine(firstDishes);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromSecondCart(int Id, string returnUrl)
        {
            SecondDishes secondDishes = mealRepository.SecondDishes
            .FirstOrDefault(p => p.Id == Id);
            if (secondDishes != null)
            {
                cart.RemoveLine(secondDishes);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromSaladCart(int Id, string returnUrl)
        {
            Salads salads = mealRepository.Salads
            .FirstOrDefault(p => p.Id == Id);
            if (salads != null)
            {
                cart.RemoveLine(salads);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}