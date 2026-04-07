using module2.Data;
using module2.Entities;

namespace module2.seed
{
    internal class DatabaseInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Clients.Any()) return;

            var customers = new[]
            {
            new Client { Name = "Pepe perez", Email = "pepe@email.com" },
            new Client { Name = "Maria Smith", Email = "maria@email.com" },
            new Client { Name = "Bob Johnson", Email = "bob@email.com" },
            new Client { Name = "Dora la exploradora", Email = "dora@email.com" },
            new Client { Name = "Pipe Bueno", Email = "pipe@email.com" },
            new Client { Name = "Maria castaño", Email = "maria@email.com" }

        };

            context.Clients.AddRange(customers);
            context.SaveChanges();

            var orders = new[]
            {
            new Order { ClientId = 1, Date = DateTime.Now.AddDays(-4), Total = 100 },
            new Order { ClientId = 1, Date = DateTime.Now.AddDays(-4), Total = 200 },
            new Order { ClientId = 2, Date = DateTime.Now.AddDays(-2), Total = 100 },
            new Order { ClientId = 2, Date = DateTime.Now.AddDays(-45), Total = 222 },
            new Order { ClientId = 3, Date = DateTime.Now.AddDays(-45), Total = 30 },
            new Order { ClientId = 4, Date = DateTime.Now.AddDays(-45), Total = 30 },
            new Order { ClientId = 6, Date = DateTime.Now.AddDays(-45), Total = 30 },
            new Order { ClientId = 5, Date = DateTime.Now.AddDays(-45), Total = 3000 },
            new Order { ClientId = 5, Date = DateTime.Now.AddDays(-45), Total = 30 },
            new Order { ClientId = 5, Date = DateTime.Now.AddDays(-20), Total = 30 }

        };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
