using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Moq;
using Xunit;
using WebDinner.Models;
using WebDinner.Controllers;
using WebDinner.Models.ViewModels;
using WebDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace WebDinner.Test
{


    public class FakeMealRepository : IMealRepository
    {
        public IEnumerable<FirstDishes> FirstDishes { get; set; }
        public IEnumerable<SecondDishes> SecondDishes { get; set; }
        public IEnumerable<Salads> Salads { get; set; }
        public IEnumerable<Sandviches> Sandviches { get; set; }

        List<Models.ViewModels.AllMeals> IMealRepository.GetAllMeals()
        {
            throw new NotImplementedException();
        }

        public void SaveMeal(Models.ViewModels.AllMeals allMeals)
        {
            throw new NotImplementedException();
        }

        public void CreateMeal(Models.ViewModels.AllMeals allMeals)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeFoodRepository : IFoodRepository
    {
        public PagedList<FirstDishes> GetFirstDish(QueryOptions options)
        {
            throw new NotImplementedException();
        }

        public PagedList<Salads> GetSalads(QueryOptions options)
        {
            throw new NotImplementedException();
        }

        public PagedList<SecondDishes> GetSecondDishes(QueryOptions options)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return GetAllMeals();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private AllMeals[] GetAllMeals()
        {
            return new AllMeals[]
            {
                new AllMeals() {Name = "N1", Price = 20},
                new AllMeals() {Name = "N2", Price = 40}
            };
        }
    }

    public class CartControllerTest
    {
        [Fact]
        public void GetAllMealsTest()
        {
            AllMeals[] allMeals = new AllMeals[]
            {
                new AllMeals() {Name = "N1", Price = 20},
                new AllMeals() {Name = "N2", Price = 40}
            };
            Mock<IMealRepository> mock = new Mock<IMealRepository>();
            mock.Setup(m => m.GetAllMeals()).Returns(allMeals.ToList());

            var tempOne = mock.Object.GetAllMeals();
            
            Assert.Equal("N1", tempOne[0].Name);
        }

        [Fact]
        public void CreateMealTest()
        {
            AllMeals allMeals = new AllMeals()
            {
                Description = "",
                Name = "N3",
                Price = 100,
                MealType = ""
            };
            Mock<IMealRepository> mock = new Mock<IMealRepository>();

            mock.Setup(x => x.
        }
    }
}
