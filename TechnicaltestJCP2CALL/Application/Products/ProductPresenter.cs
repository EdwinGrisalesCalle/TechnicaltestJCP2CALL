using System;
using System.ComponentModel;
using System.Linq;
using TechnicaltestJCP2CALL.Domain;
using TechnicaltestJCP2CALL.Infrastructure;
using TechnicaltestJCP2CALL.Validators;

namespace TechnicaltestJCP2CALL.Application.Products
{
    public class ProductPresenter
    {
        private readonly IProductView view;
        private readonly IProductService service;

        private Product selectedProduct = null;

        public ProductPresenter(IProductView view, IProductService service)
        {
            this.view = view;
            this.service = service;
        }

        public void Initialize()
        {
            view.RefreshGrid(service.GetAll());
        }
        public void Save()
        {
            // Parsear precio y stock
            if (!decimal.TryParse(view.PriceInput, out decimal price) ||
                !int.TryParse(view.StockInput, out int stock))
            {
                view.ShowMessage("Precio o Stock inválidos");
                return;
            }

            string nameInput = view.NameInput.Trim();

            // Validar nombre único en los productos existentes
            bool nameExists = service.GetAll()
                .Any(p => p.Name.Equals(nameInput, StringComparison.OrdinalIgnoreCase));

            if (nameExists)
            {
                view.ShowMessage("El nombre del producto ya existe.");
                return;
            }

            // Validaciones de negocio: nombre no vacío, precio > 0, stock >= 0
            string error = ProductValidator.Validate(nameInput, price, stock);

            if (error != null)
            {
                view.ShowMessage(error);
                return;
            }

            // Guardar nuevo producto
            service.Add(new Product
            {
                Name = nameInput,
                Price = price,
                Stock = stock
            });

            // Limpiar campos y refrescar grid
            view.ClearFields();
            view.RefreshGrid(service.GetAll());
        }

        public void Delete(Product product)
        {
            service.Remove(product);
            view.ClearFields();
            view.RefreshGrid(service.GetAll());
        }

        public void Selected(Product product)
        {
            selectedProduct = product;
        }

        public void Search()
        {
            var filtered = service.GetAll()
                .Where(p => p.Name.ToLower().Contains(view.SearchInput.ToLower()))
                .ToList();

            view.RefreshGrid(new BindingList<Product>(filtered));
        }
    }
}
