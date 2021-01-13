using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Xunit;
using WebDinner.Models;
using WebDinner.Controllers;
using WebDinner.Models.ViewModels;
using WebDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebDinner.Test
{
    

    public class DishData
    {
        public IQueryable<FirstDishes> FirstDishes { get; set; }

        public DishData()
        {
            FirstDishes = new List<FirstDishes>()
            {
                new FirstDishes() { Name = "One", Price = 10, Description = "", CreatingDate = DateTime.Now, Id = 1, PhotoName = ""},
                new FirstDishes() { Name = "Two", Price = 20, Description = "", CreatingDate = DateTime.Now, Id = 1, PhotoName = ""},
                new FirstDishes() { Name = "Three", Price = 10, Description = "", CreatingDate = DateTime.Now, Id = 1, PhotoName = ""},
                new FirstDishes() { Name = "One", Price = 50, Description = "", CreatingDate = DateTime.Now, Id = 1, PhotoName = ""},
                new FirstDishes() { Name = "One", Price = 10, Description = "", CreatingDate = DateTime.Now, Id = 1, PhotoName = ""},
                new FirstDishes() { Name = "One", Price = 10, Description = "", CreatingDate = DateTime.Now, Id = 1, PhotoName = ""}
            }.AsQueryable<FirstDishes>();
        }
    }
    public class HomeControllerTests
    {
        [Fact]
        public void CanPaginate()
        {
            Mock<IFoodRepository> mock = new Mock<IFoodRepository>();
            QueryOptions options = new QueryOptions()
            {
                CurrentPage = 1,
                PageSize = 3
            };
            DishData dishData = new DishData();

            PagedList<FirstDishes> dishes = new PagedList<FirstDishes>(dishData.FirstDishes, options);

            mock.Setup(m => m.GetFirstDish(options)).Returns(dishes);

            var temp = mock.Object.GetFirstDish(options).ToArray();
            
            FirstDishes[] firsts = temp.ToArray();
            
            Assert.True(firsts.Length == 3);
            Assert.Equal("Three", firsts[2].Name);
            Assert.Equal(20, firsts[1].Price);
        }
    }
}
