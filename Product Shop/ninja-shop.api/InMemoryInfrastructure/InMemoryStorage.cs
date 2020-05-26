using System.Collections.Generic;
using Product_Shop.core.Models;

namespace Product_Shop.api.InMemoryInfrastructure
{
    public sealed class InMemoryStorage : IDataContext
    {
        static InMemoryStorage() { }

        InMemoryStorage() { }

        public static InMemoryStorage Instance { get; private set; } = new InMemoryStorage();

        public IList<Product> Products { get; } = LoadProducts();
        public IList<Order> Orders { get; } = new List<Order>();
        public IList<Customer> Customers { get; } = LoadCustomers();


        private static IList<Product> LoadProducts()
        {
           return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    ProductName = "Сметана",
                    Description ="Сметана Президент",
                    HitPoints = 2,
                    Price = 30,
                    ImageUrl = "https://openclipart.org/image/800px/297175  "
                },
                new Product
                {
                    Id= 2,
                    ProductName ="Пицца",
                    Description = "Гавайская пицца — это сыр и томатная основа, кусочки канадской ветчины (бекона) и ананаса.",
                    Price = 100,
                    HitPoints= 4,
                    ImageUrl = "https://openclipart.org/image/800px/314481"
                },
                new Product
                {
                    Id= 3,
                    ProductName= "Булочка",
                    Description= "Свежая булочка с маком",
                    Price= 8,
                    HitPoints= 4,
                    ImageUrl= "https://openclipart.org/image/800px/209532"
                },
                new Product
                {
                    Id= 4,
                    ProductName= "Мороженное",
                    Description= "Рожек-пломбир Белая Береза",
                    Price= 20,
                    HitPoints= 3,
                    ImageUrl= "https://openclipart.org/image/800px/303081"
                },
                new Product
                {
                    Id= 5,
                    ProductName= "Чипсы",
                    Description= "Лейс со вкусом краба",
                    Price= 35,
                    HitPoints= 4,
                    ImageUrl= "https://openclipart.org/image/800px/268435"
                },
            };
        }
        
      
        private static IList<Customer> LoadCustomers()
        {
         return new List<Customer>
         {
             new Customer{FirstName = "Саша",LastName = "Хоменко", Id = 1,Email = "kpi@fict.com"}
         };
        }
    }
}