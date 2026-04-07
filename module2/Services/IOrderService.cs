using module2.Dto;
using module2.Entities;

namespace module2.Services
{
    public interface IOrderService
    {
        void InsertNewOrder();
        List<OrderDto> GetOrdersFromLastMonth();
        ClientSpendingDto GetTopCustomer();
    }
}

