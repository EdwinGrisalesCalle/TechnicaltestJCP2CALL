namespace TechnicaltestJCP2CALL.Application.Products
{
    public interface IProductView
    {
        string NameInput { get; }
        string PriceInput { get; }
        string StockInput { get; }
        string SearchInput { get; }

        void ShowMessage(string message);
        void RefreshGrid(object data);
        void ClearFields();
    }
}
