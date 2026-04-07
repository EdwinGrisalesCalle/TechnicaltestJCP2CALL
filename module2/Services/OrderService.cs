using Microsoft.EntityFrameworkCore;
using module2.Data;
using module2.Dto;
using module2.Entities;

namespace module2.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public void InsertNewOrder()
        {
            var customer = _context.Clients.FirstOrDefault();

            if (customer == null) return;

            var order = new Order
            {
                ClientId = customer.Id,
                Date = DateTime.Now,
                Total = 1
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public List<OrderDto> GetOrdersFromLastMonth()
        {
            var oneMonthAgo = DateTime.Now.AddMonths(-1);

            return _context.Orders
                .AsNoTracking()
                .Where(o => o.Date >= oneMonthAgo)
                .Select(o => new OrderDto
                {
                    ClientName = o.Client.Name,
                    Date = o.Date,
                    Total = o.Total
                })
                .OrderByDescending(o => o.Date)
                .ToList();
        }

        public ClientSpendingDto GetTopCustomer()
        {
            return _context.Clients
                .AsNoTracking()
                .Select(c => new ClientSpendingDto
                {
                    Name = c.Name,
                    Email = c.Email,
                    TotalSpent = c.Orders.Sum(o => o.Total),
                    OrderCount = c.Orders.Count()
                })
                .OrderByDescending(c => c.TotalSpent)
                .FirstOrDefault();
        }
    }
}
