using System.ComponentModel;
using TechnicaltestJCP2CALL.Domain;

namespace TechnicaltestJCP2CALL.Infrastructure.Products
{
    public class ProductService : IProductService
    {
        private BindingList<Product> products = new BindingList<Product>();

        public BindingList<Product> GetAll()
        {
            return products;
        }

        public void Add(Product product)
        {
            products.Add(product);
        }

        public void Remove(Product product)
        {
            if (product != null)
                products.Remove(product);
        }
    }
}
