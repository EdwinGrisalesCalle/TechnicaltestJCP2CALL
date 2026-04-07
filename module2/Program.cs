using Microsoft.EntityFrameworkCore;
using module2.Data;
using module2.Entities;
using module2.Presentation;
using module2.seed;
using module2.Services;

namespace TechnicalTestEF
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLitePCL.Batteries.Init();

            using var context = new AppDbContext();

            DatabaseInitializer.Initialize(context);

            var service = new OrderService(context);
            var ui = new ConsoleUI(service);

            ui.Run();

            Console.ReadKey();
        }

    }
}