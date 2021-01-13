using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDinner.Models
{
    public class Order
    {
        [BindNever]
        public int Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите имя")]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Пожалуйста введите адрес")]
        public string Address { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [BindNever]
        [MaxLength(500)]
        public string OrderDescription { get; set; }
        [Required]
        public bool Snipped { get; set; }
    }
}
