using System.ComponentModel;
using TechnicaltestJCP2CALL.Domain;

namespace TechnicaltestJCP2CALL.Infrastructure
{
    public interface IProductService
    {
        BindingList<Product> GetAll();
        void Add(Product product);
        void Remove(Product product);
    }
}
