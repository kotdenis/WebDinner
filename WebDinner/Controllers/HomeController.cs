using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDinner.Models;
using WebDinner.Models.ViewModels;
using WebDinner.Infrastructure;

namespace WebDinner.Controllers
{
    public class HomeController : Controller
    {
        IMealRepository mealRepository;
        IFoodRepository foodRepository;

        public HomeController(IMealRepository mealRepository, IFoodRepository foodRepository)
        {
            this.mealRepository = mealRepository;
            this.foodRepository = foodRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFirstDishes(QueryOptions options)
        {
            return View(foodRepository.GetFirstDish(options));
        }

        public IActionResult GetSecondDishes(QueryOptions options)
        {
            return View(foodRepository.GetSecondDishes(options));
        }

        public IActionResult MenuSide(QueryOptions options)
        {
            return View(mealRepository);
        }

        public IActionResult IndexTest()
        {
            return View();
        }
    }
}