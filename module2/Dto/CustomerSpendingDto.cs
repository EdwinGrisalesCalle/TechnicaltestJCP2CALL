namespace module2.Dto
{
    public class ClientSpendingDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal TotalSpent { get; set; }
        public int OrderCount { get; set; }

        public override string ToString()
        {
            return $"Cliente: {Name}, Email: {Email}, Total: {TotalSpent:C}, Cantitad total: {OrderCount}";
        }
    }
}
