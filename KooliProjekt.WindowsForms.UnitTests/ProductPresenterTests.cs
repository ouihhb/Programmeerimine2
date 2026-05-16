using KooliProjekt.WindowsForms;
using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms.UnitTests
{
    public class ProductPresenterTests
    {
        [Fact]
        public async Task LoadData_should_call_ShowError_with_faulty_response()
        {
            var view = new TestProductsView();
            var apiClient = new TestApiClient
            {
                ListResult = new OperationResult<List<Product>>().AddError("Load failed")
            };
            var presenter = new ProductPresenter(view, apiClient);

            await presenter.LoadData();

            Assert.Equal("Load failed", view.LastError);
        }

        [Fact]
        public async Task LoadData_should_set_DataSource_with_valid_response()
        {
            var products = new List<Product> { new Product { Id = 1, Name = "Keyboard" } };
            var view = new TestProductsView();
            var apiClient = new TestApiClient
            {
                ListResult = new OperationResult<List<Product>>(products)
            };
            var presenter = new ProductPresenter(view, apiClient);

            await presenter.LoadData();

            Assert.Same(products, view.DataSource);
        }

        [Fact]
        public void SetSelection_should_clear_fields_with_null_selection()
        {
            var view = new TestProductsView
            {
                ProductId = 7,
                ProductName = "Mouse",
                ProductDescription = "Wireless",
                ProductPrice = 15
            };
            var presenter = new ProductPresenter(view, new TestApiClient());

            presenter.SetSelection(null);

            Assert.Equal(0, view.ProductId);
            Assert.Equal(string.Empty, view.ProductName);
            Assert.Equal(string.Empty, view.ProductDescription);
            Assert.Equal(0, view.ProductPrice);
        }

        [Fact]
        public void SetSelection_should_set_fields_with_valid_selection()
        {
            var view = new TestProductsView();
            var presenter = new ProductPresenter(view, new TestApiClient());

            presenter.SetSelection(new Product
            {
                Id = 3,
                Name = "Monitor",
                Description = "27 inch",
                Price = 199
            });

            Assert.Equal(3, view.ProductId);
            Assert.Equal("Monitor", view.ProductName);
            Assert.Equal("27 inch", view.ProductDescription);
            Assert.Equal(199, view.ProductPrice);
        }

        [Fact]
        public async Task Save_should_call_ShowError_with_faulty_response()
        {
            var view = new TestProductsView();
            var apiClient = new TestApiClient
            {
                SaveResult = new OperationResult<int>().AddError("Save failed")
            };
            var presenter = new ProductPresenter(view, apiClient);

            await presenter.Save();

            Assert.Equal("Save failed", view.LastError);
        }

        [Fact]
        public async Task Save_should_call_LoadData_with_valid_response()
        {
            var view = new TestProductsView();
            var apiClient = new TestApiClient
            {
                SaveResult = new OperationResult<int>(4),
                ListResult = new OperationResult<List<Product>>(new List<Product>())
            };
            var presenter = new ProductPresenter(view, apiClient);

            await presenter.Save();

            Assert.Equal(1, apiClient.ListCalls);
        }

        [Fact]
        public async Task Delete_should_return_when_user_didnot_confirmed()
        {
            var view = new TestProductsView
            {
                ProductId = 2,
                ConfirmDeleteResult = false
            };
            var apiClient = new TestApiClient();
            var presenter = new ProductPresenter(view, apiClient);

            await presenter.Delete();

            Assert.Equal(0, apiClient.DeleteCalls);
        }

        [Fact]
        public async Task Delete_should_call_ShowError_with_faulty_response()
        {
            var view = new TestProductsView
            {
                ProductId = 2,
                ConfirmDeleteResult = true
            };
            var apiClient = new TestApiClient
            {
                DeleteResult = new OperationResult().AddError("Delete failed")
            };
            var presenter = new ProductPresenter(view, apiClient);

            await presenter.Delete();

            Assert.Equal("Delete failed", view.LastError);
        }

        private sealed class TestProductsView : IProductsView
        {
            public object? DataSource { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public string ProductDescription { get; set; } = string.Empty;
            public decimal ProductPrice { get; set; }
            public string? LastError { get; private set; }
            public bool ConfirmDeleteResult { get; set; } = true;

            public void ShowError(string message)
            {
                LastError = message;
            }

            public bool ConfirmDelete()
            {
                return ConfirmDeleteResult;
            }
        }

        private sealed class TestApiClient : IApiClient
        {
            public OperationResult<List<Product>> ListResult { get; set; } = new(new List<Product>());
            public OperationResult<int> SaveResult { get; set; } = new(1);
            public OperationResult DeleteResult { get; set; } = new();
            public int ListCalls { get; private set; }
            public int DeleteCalls { get; private set; }

            public Task<OperationResult<List<Product>>> List()
            {
                ListCalls++;
                return Task.FromResult(ListResult);
            }

            public Task<OperationResult<int>> Save(Product product)
            {
                return Task.FromResult(SaveResult);
            }

            public Task<OperationResult> Delete(int id)
            {
                DeleteCalls++;
                return Task.FromResult(DeleteResult);
            }
        }
    }
}
