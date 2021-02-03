using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Interface
{
    public class Item
    {
        public Item(string ItemType)
        {
            switch (ItemType)
            {
                case "A":
                case "a":
                    this.Id = "A";
                    this.Price = 50m;
                    break;
                case "B":
                case "b":
                    this.Id = "B";
                    this.Price = 30m;
                    break;
                case "C":
                case "c":
                    this.Id = "C";
                    this.Price = 20m;
                    break;
                case "D":
                case "d":
                    this.Id = "D";
                    this.Price = 2015m;
                    break;
            }
        }
        public string Id { get; set; }
        public decimal Price { get; set; }
    }
}
