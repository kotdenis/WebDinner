using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDinner.Models
{
    public class Cart
    {
        List<CartLine<FirstDishes>> firstDishes = new List<CartLine<FirstDishes>>();
        List<CartLine<SecondDishes>> secondDishes = new List<CartLine<SecondDishes>>();
        List<CartLine<Salads>> salads = new List<CartLine<Salads>>();

        public virtual void AddItem(dynamic meal, int quantity)
        {
            if (meal.GetType() == typeof(FirstDishes))
            {
                CartLine<FirstDishes> cart = firstDishes
                    .Where(x => x.Meal.Id == meal.Id)
                    .FirstOrDefault();
                if (cart == null)
                {
                    firstDishes.Add(new CartLine<FirstDishes>()
                    {
                        Meal = meal,
                        Quantity = quantity
                    });
                }
                else
                    cart.Quantity += quantity;
            }
            if(meal.GetType() == typeof(SecondDishes))
            {
                CartLine<SecondDishes> cart = secondDishes
                    .Where(x => x.Meal.Id == meal.Id)
                    .FirstOrDefault();
                if (cart == null)
                {
                    secondDishes.Add(new CartLine<SecondDishes>()
                    {
                        Meal = meal,
                        Quantity = quantity
                    });
                }
                else
                    cart.Quantity += quantity;
            }
            if (meal.GetType() == typeof(Salads))
            {
                CartLine<Salads> cart = salads
                    .Where(x => x.Meal.Id == meal.Id)
                    .FirstOrDefault();
                if (cart == null)
                {
                    salads.Add(new CartLine<Salads>()
                    {
                        Meal = meal,
                        Quantity = quantity
                    });
                }
                else
                    cart.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(dynamic meal)
        {
            if (meal.GetType() == typeof(FirstDishes))
            {
                firstDishes.RemoveAll(x => x.Meal.Id == meal.Id);
            }
            if (meal.GetType() == typeof(SecondDishes))
            {
                secondDishes.RemoveAll(x => x.Meal.Id == meal.Id);
            }
            if (meal.GetType() == typeof(Salads))
            {
                salads.RemoveAll(x => x.Meal.Id == meal.Id);
            }
        }

        public virtual decimal ComputeTotalValue()
        {
            var first = (decimal)firstDishes.Sum(x => x.Meal.Price * x.Quantity);
            var second = (decimal)secondDishes.Sum(x => x.Meal.Price * x.Quantity);
            return first + second;
        }

        public virtual void Clear()
        {
            firstDishes.Clear();
            secondDishes.Clear();
            salads.Clear();
        }

        
        public IEnumerable<CartLine<FirstDishes>> FirstDishes => firstDishes;
        public IEnumerable<CartLine<SecondDishes>> SecondDishes => secondDishes;
        public IEnumerable<CartLine<Salads>> Salads => salads;
    }

    public class CartLine<T>
    {
        public int CartLineID { get; set; }
        public T Meal { get; set; }
        public int Quantity { get; set; }
    }
}
