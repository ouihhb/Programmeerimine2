using KooliProjekt.WpfApplication;
using KooliProjekt.WpfApplication.Api;

namespace KooliProjekt.WpfApplication.UnitTests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public async Task LoadData_should_show_error_when_api_fails()
        {
            var apiClient = new TestApiClient
            {
                ListResult = new OperationResult<List<Product>>().AddError("Load failed")
            };
            var dialogs = new TestDialogProvider();
            var viewModel = new MainWindowViewModel(apiClient, dialogs);

            await viewModel.LoadData();

            Assert.Equal("Load failed", dialogs.LastError);
        }

        [Fact]
        public async Task LoadData_should_fill_products_when_api_succeeds()
        {
            var apiClient = new TestApiClient
            {
                ListResult = new OperationResult<List<Product>>(new List<Product>
                {
                    new Product { Id = 1, Name = "Keyboard" }
                })
            };
            var viewModel = new MainWindowViewModel(apiClient, new TestDialogProvider());

            await viewModel.LoadData();

            Assert.Single(viewModel.Products);
            Assert.Equal("Keyboard", viewModel.Products[0].Name);
        }

        [Fact]
        public void SelectedProduct_should_update_edit_fields()
        {
            var viewModel = new MainWindowViewModel(new TestApiClient(), new TestDialogProvider());

            viewModel.SelectedProduct = new Product
            {
                Id = 7,
                Name = "Mouse",
                Description = "Wireless",
                Price = 25
            };

            Assert.Equal(7, viewModel.ProductId);
            Assert.Equal("Mouse", viewModel.ProductName);
            Assert.Equal("Wireless", viewModel.ProductDescription);
            Assert.Equal(25, viewModel.ProductPrice);
        }

        [Fact]
        public void AddNew_should_clear_edit_fields()
        {
            var viewModel = new MainWindowViewModel(new TestApiClient(), new TestDialogProvider())
            {
                ProductId = 5,
                ProductName = "Monitor",
                ProductDescription = "Old",
                ProductPrice = 100
            };

            viewModel.AddNew();

            Assert.Equal(0, viewModel.ProductId);
            Assert.Equal(string.Empty, viewModel.ProductName);
            Assert.Equal(string.Empty, viewModel.ProductDescription);
            Assert.Equal(0, viewModel.ProductPrice);
        }

        [Fact]
        public async Task Save_should_reload_products_when_api_succeeds()
        {
            var apiClient = new TestApiClient
            {
                SaveResult = new OperationResult<int>(1),
                ListResult = new OperationResult<List<Product>>(new List<Product>())
            };
            var viewModel = new MainWindowViewModel(apiClient, new TestDialogProvider());

            await viewModel.Save();

            Assert.Equal(1, apiClient.ListCalls);
        }

        [Fact]
        public async Task Delete_should_not_call_api_when_user_cancels()
        {
            var apiClient = new TestApiClient();
            var dialogs = new TestDialogProvider { ConfirmDeleteResult = false };
            var viewModel = new MainWindowViewModel(apiClient, dialogs)
            {
                ProductId = 3
            };

            await viewModel.Delete();

            Assert.Equal(0, apiClient.DeleteCalls);
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

        private sealed class TestDialogProvider : IDialogProvider
        {
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
    }
}
