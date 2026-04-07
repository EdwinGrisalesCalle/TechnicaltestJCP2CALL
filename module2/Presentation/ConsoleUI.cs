using module2.Services;

namespace module2.Presentation
{
    public class ConsoleUI
    {
        private readonly IOrderService _service;

        public ConsoleUI(IOrderService service)
        {
            _service = service;
        }

        public void Run()
        {
            Console.WriteLine("=== RESULTADO DE PROCESO ===\n");

            var orders = _service.GetOrdersFromLastMonth();
            Console.WriteLine("pedidos del último mes, mostrando nombre del cliente, fecha y total\n");
            foreach (var o in orders)
            {
                Console.WriteLine(o);
            }

            var topClient = _service.GetTopCustomer();
            Console.WriteLine("\nCliente con mayor gasto total (suma de sus pedidos):\n");
            Console.WriteLine(topClient);

            _service.InsertNewOrder();
            Console.WriteLine("\nOrden insertada.\n");

        }
    }
}
