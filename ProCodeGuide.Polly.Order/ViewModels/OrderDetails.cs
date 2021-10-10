using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProCodeGuide.Polly.Order.ViewModels
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime SetupDate { get; set; }
        public List<Item> Items { get; set; }
    }
}
