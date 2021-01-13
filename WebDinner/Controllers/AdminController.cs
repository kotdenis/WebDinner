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
    public class AdminController : Controller
    {
        private IMealRepository mealRepository;

        public AdminController(IMealRepository mealRepository)
        {
            this.mealRepository = mealRepository;
        }

        public IActionResult Index() => View(mealRepository.GetAllMeals());

        public ViewResult Edit(int id, string name, Type meal)
        {
            return View(mealRepository.GetAllMeals().FirstOrDefault(x => x.Id == id && x.Name == name));
        }

        [HttpPost]
        public IActionResult Edit(AllMeals allMeals)
        {
            if (ModelState.IsValid)
            {
                mealRepository.SaveMeal(allMeals);
                TempData["message"] = $"{allMeals.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
                return View(allMeals);
        }

        public ViewResult Create() => View(new AllMeals());

        [HttpPost]
        public IActionResult Create(AllMeals allMeals)
        {
            if (ModelState.IsValid)
            {
                mealRepository.CreateMeal(allMeals);
                TempData["message"] = $"{allMeals.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
                return View(allMeals);
        }
    }
}