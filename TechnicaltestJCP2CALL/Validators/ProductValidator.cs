namespace TechnicaltestJCP2CALL.Validators
{
    public static class ProductValidator
    {
        public static string Validate(string name, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "El nombre es requerido";

            if (price <= 0)
                return "El precio debe ser mayor a 0";

            if (stock < 0)
                return "El stock mayor o igual a 0";

            return null;
        }
    }
}
