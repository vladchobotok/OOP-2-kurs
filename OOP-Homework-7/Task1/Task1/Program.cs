using System;
using System.Collections.Generic;

namespace Task1
{
    class Program
    {
        //SRP principe (виконує одразу 3 задачі: керує елементами списку, друкує інформацію, працює з БД)
        class Item
        {

        }
        class Order
        {
            private List<Item> itemList;

            internal List<Item> ItemList
            {
                get
                {
                    return itemList;
                }

                set
                {
                    itemList = value;
                }
            }
            public void CalculateTotalSum() {/*...*/}
            public void GetItems() {/*...*/}
            public void GetItemCount() {/*...*/}
            public void AddItem(Item item) {/*...*/}
            public void DeleteItem(Item item) {/*...*/}
        }
        class OrderManager
        {
            public void PrintOrder() {/*...*/}
            public void ShowOrder() {/*...*/}
        }
        class OrderUpdater
        {
            public void Load() {/*...*/}
            public void Save() {/*...*/}
            public void Update() {/*...*/}
            public void Delete() {/*...*/}
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
