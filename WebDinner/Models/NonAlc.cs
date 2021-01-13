using System;
using System.Collections.Generic;

namespace WebDinner.Models
{
    public partial class NonAlc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatingDate { get; set; }
    }
}
