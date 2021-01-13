using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebDinner.Infrastructure;

namespace WebDinner.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(dynamic meal, int quantity)
        {
            if(meal.GetType() == typeof(FirstDishes)) 
                base.AddItem(meal as FirstDishes, quantity);
            if (meal.GetType() == typeof(SecondDishes))
                base.AddItem(meal as SecondDishes, quantity);
            if (meal.GetType() == typeof(Salads))
                base.AddItem(meal as Salads, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(dynamic meal)
        {
            if (meal.GetType() == typeof(FirstDishes))
                base.RemoveLine(meal as FirstDishes);
            if (meal.GetType() == typeof(SecondDishes))
                base.RemoveLine(meal as SecondDishes);
            if (meal.GetType() == typeof(Salads))
                base.RemoveLine(meal as Salads);

            Session.SetJson("Cart", this);
        }

        //public override void Clear()
        //{
        //    base.Clear();
        //    Session.Remove("Cart");
        //}
    }
}
