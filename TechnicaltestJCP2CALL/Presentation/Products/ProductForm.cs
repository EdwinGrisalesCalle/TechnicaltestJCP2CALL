using System;
using System.Windows.Forms;
using TechnicaltestJCP2CALL.Application.Products;
using TechnicaltestJCP2CALL.Domain;

namespace product_manager_winforms.Presentation.Products
{
    public partial class ProductForm : Form, IProductView
    {

        private ProductPresenter presenter;
        private bool suppressSelectionChanged = false;

        public ProductForm()
        {
            InitializeComponent();
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
        }

        public void SetPresenter(ProductPresenter presenter)
        {
            this.presenter = presenter;
        }

        public string NameInput => txtName.Text;
        public string PriceInput => txtPrice.Text;
        public string StockInput => txtStock.Text;
        public string SearchInput => txtSearch.Text;


        private void ProductForm_Load(object sender, EventArgs e)
        {
            presenter?.Initialize();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            presenter?.Save();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {   
            if (dgvProducts.CurrentRow == null) return;

            var product = (Product)dgvProducts.CurrentRow.DataBoundItem;
            presenter.Delete(product);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            presenter?.Search();
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (suppressSelectionChanged) return;

            if (dgvProducts.CurrentRow == null)
            {
                btnDelete.Enabled = false;
                return;
            }

            var product = dgvProducts.CurrentRow.DataBoundItem as Product;
            if (product == null) return;

            // Cargar datos en los campos solo si hay selección manual
            txtName.Text = product.Name;
            txtPrice.Text = product.Price.ToString();
            txtStock.Text = product.Stock.ToString();

            //btnDelete.Enabled = true;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void RefreshGrid(object data)
        {
            suppressSelectionChanged = true;

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = data;

            suppressSelectionChanged = false;
        }

        public void ClearFields()
        {
            txtName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
        }

    }
}
