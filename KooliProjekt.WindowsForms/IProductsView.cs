namespace KooliProjekt.WindowsForms
{
    public interface IProductsView
    {
        object? DataSource { get; set; }
        int ProductId { get; set; }
        string ProductName { get; set; }
        string ProductDescription { get; set; }
        decimal ProductPrice { get; set; }

        void ShowError(string message);
        bool ConfirmDelete();
    }
}
