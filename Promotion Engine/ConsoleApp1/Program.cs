using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.BAL;
using ConsoleApp1.Interface;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> items = new List<Item>();
            IItemService itemService = new ItemService();
            Console.WriteLine("Please enter total number of orders");
            int a = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < a; i++)
            {
                Console.WriteLine("Please enter the type of product:A,B,C or D");
                string type = Console.ReadLine();
                Item item = new Item(type);
                items.Add(item);
            }

            int totalPrice = itemService.GetTotalPrice(items);
            Console.WriteLine(totalPrice);
            Console.ReadLine();

        }
    }
}
