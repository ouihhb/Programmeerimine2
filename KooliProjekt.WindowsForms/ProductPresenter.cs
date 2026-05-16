using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms
{
    public class ProductPresenter
    {
        private readonly IProductsView _view;
        private readonly IApiClient _apiClient;

        public ProductPresenter(IProductsView view, IApiClient apiClient)
        {
            _view = view;
            _apiClient = apiClient;
        }

        public async Task LoadData()
        {
            var result = await _apiClient.List();

            if (result.HasErrors)
            {
                _view.ShowError(result.GetErrorMessage());
                return;
            }

            _view.DataSource = result.Value ?? new List<Product>();
        }

        public void SetSelection(Product? product)
        {
            if (product == null)
            {
                _view.ProductId = 0;
                _view.ProductName = string.Empty;
                _view.ProductDescription = string.Empty;
                _view.ProductPrice = 0;
                return;
            }

            _view.ProductId = product.Id;
            _view.ProductName = product.Name;
            _view.ProductDescription = product.Description;
            _view.ProductPrice = product.Price;
        }

        public void AddNew()
        {
            SetSelection(null);
        }

        public async Task Save()
        {
            var product = new Product
            {
                Id = _view.ProductId,
                Name = _view.ProductName,
                Description = _view.ProductDescription,
                Price = _view.ProductPrice
            };

            var result = await _apiClient.Save(product);

            if (result.HasErrors)
            {
                _view.ShowError(result.GetErrorMessage());
                return;
            }

            await LoadData();
        }

        public async Task Delete()
        {
            if (_view.ProductId <= 0)
            {
                return;
            }

            if (!_view.ConfirmDelete())
            {
                return;
            }

            var result = await _apiClient.Delete(_view.ProductId);

            if (result.HasErrors)
            {
                _view.ShowError(result.GetErrorMessage());
                return;
            }

            AddNew();
            await LoadData();
        }
    }
}
