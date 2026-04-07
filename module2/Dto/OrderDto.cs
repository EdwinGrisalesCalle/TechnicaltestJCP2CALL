namespace module2.Dto
{
    public class OrderDto
    {
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }

        public override string ToString()
        {
            return $"Cliente: {ClientName}, Fecha: {Date:dd/MM/yyyy}, Total: {Total:C}";
        }
    }
}
