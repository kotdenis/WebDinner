using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDinner.Models;
using System.Collections;
using WebDinner.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace WebDinner.Models.ViewModels
{

    public interface IMealRepository
    {
        IEnumerable<FirstDishes> FirstDishes { get; }
        IEnumerable<SecondDishes> SecondDishes { get; }
        IEnumerable<Salads> Salads { get; }
        IEnumerable<Sandviches> Sandviches { get; }

        List<AllMeals> GetAllMeals();
        void SaveMeal(AllMeals allMeals);
        void CreateMeal(AllMeals allMeals);
    }

    public class MealRepository : IMealRepository
    {
        ProductsDBContext dBContext;
        public MealRepository(ProductsDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public IEnumerable<FirstDishes> FirstDishes => dBContext.FirstDishes;
        public IEnumerable<SecondDishes> SecondDishes => dBContext.SecondDishes;
        public IEnumerable<Salads> Salads => dBContext.Salads;
        public IEnumerable<Sandviches> Sandviches => dBContext.Sandviches;

        public List<AllMeals> GetAllMeals()
        {
            List<AllMeals> allMeals = new List<AllMeals>();
            if (dBContext.FirstDishes.Count() > 0)
                foreach (var t in dBContext.FirstDishes)
                    allMeals.Add(new AllMeals
                    {
                        Name = t.Name,
                        Id = t.Id,
                        Price = t.Price,
                        Description = t.Description,
                        MealType = "FirstDishes"
                    });
            if (dBContext.SecondDishes.Count() > 0)
                foreach (var t in dBContext.SecondDishes)
                    allMeals.Add(new AllMeals
                    {
                        Name = t.Name,
                        Id = t.Id,
                        Price = t.Price,
                        Description = t.Description,
                        MealType = "SecondDishes"
                    });
            return allMeals;
        }

        public void SaveMeal(AllMeals allMeals)
        {
            var first = dBContext.FirstDishes.FirstOrDefault(x => x.Id == allMeals.Id && x.Name == allMeals.Name);
            var second = dBContext.SecondDishes.FirstOrDefault(x => x.Id == allMeals.Id && x.Name == allMeals.Name);
            var salad = dBContext.Salads.FirstOrDefault(x => x.Id == allMeals.Id && x.Name == allMeals.Name);

            if(first != null)
            {
                first.Name = allMeals.Name;
                first.Price = allMeals.Price;
                first.Description = allMeals.Description;
                dBContext.SaveChanges();
            }
            if(second != null)
            {
                second.Description = allMeals.Description;
                second.Name = allMeals.Name;
                second.Price = allMeals.Price;
                dBContext.SaveChanges();
            }
            if(salad != null)
            {
                salad.Description = allMeals.Description;
                salad.Name = allMeals.Name;
                salad.Price = allMeals.Price;
                dBContext.SaveChanges();
            }
        }

        public void CreateMeal(AllMeals allMeals)
        {
            if(allMeals != null)
            {
                if(allMeals.MealType == "firstDish")
                {
                    dBContext.FirstDishes.Add(new Models.FirstDishes
                    {
                        Description = allMeals.Description,
                        Name = allMeals.Name,
                        PhotoName = "",
                        CreatingDate = DateTime.Now,
                        Price = allMeals.Price
                    });
                }
                if (allMeals.MealType == "secondDish")
                {
                    dBContext.SecondDishes.Add(new Models.SecondDishes
                    {
                        Description = allMeals.Description,
                        Name = allMeals.Name,
                        PhotoName = "",
                        CreatingDate = DateTime.Now,
                        Price = allMeals.Price
                    });
                }
                if (allMeals.MealType == "salad")
                {
                    dBContext.Salads.Add(new Models.Salads
                    {
                        Description = allMeals.Description,
                        Name = allMeals.Name,
                        PhotoName = "",
                        CreatingDate = DateTime.Now,
                        Price = allMeals.Price
                    });
                }
            }
        }
    }

    public class AllMeals
    {
        [Required(ErrorMessage = "Пожалуйста введите наименование")]
        public string Name { get; set; }
        public int Id { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста введите цену")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите описание")]
        public string Description { get; set; }
        public string MealType { get; set; }
    }

    public interface IFoodRepository
    {
        PagedList<FirstDishes> GetFirstDish(QueryOptions options);

        PagedList<SecondDishes> GetSecondDishes(QueryOptions options);

        PagedList<Salads> GetSalads(QueryOptions options);
    }

    public class FoodRepository : IFoodRepository
    {
        ProductsDBContext dBContext;
        public FoodRepository(ProductsDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public PagedList<FirstDishes> GetFirstDish(QueryOptions options)
        {
            return new PagedList<FirstDishes>(dBContext.FirstDishes, options);
        }

        public PagedList<SecondDishes> GetSecondDishes(QueryOptions options)
        {
            return new PagedList<SecondDishes>(dBContext.SecondDishes, options);
        }

        public PagedList<Salads> GetSalads(QueryOptions options)
        {
            return new PagedList<Salads>(dBContext.Salads, options);
        }
    }
}
