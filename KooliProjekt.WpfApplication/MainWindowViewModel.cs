using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using KooliProjekt.WpfApplication.Api;

namespace KooliProjekt.WpfApplication
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        private readonly IApiClient _apiClient;
        private readonly IDialogProvider _dialogProvider;
        private Product? _selectedProduct;
        private int _productId;
        private string _productName = string.Empty;
        private string _productDescription = string.Empty;
        private decimal _productPrice;

        public MainWindowViewModel() : this(new ApiClient(), new DialogProvider())
        {
        }

        public MainWindowViewModel(IApiClient apiClient, IDialogProvider dialogProvider)
        {
            _apiClient = apiClient;
            _dialogProvider = dialogProvider;

            AddNewCommand = new RelayCommand(AddNew);
            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
        }

        public ObservableCollection<Product> Products { get; } = new();

        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (SetProperty(ref _selectedProduct, value))
                {
                    SetSelection(value);
                }
            }
        }

        public int ProductId
        {
            get => _productId;
            set => SetProperty(ref _productId, value);
        }

        public string ProductName
        {
            get => _productName;
            set => SetProperty(ref _productName, value);
        }

        public string ProductDescription
        {
            get => _productDescription;
            set => SetProperty(ref _productDescription, value);
        }

        public decimal ProductPrice
        {
            get => _productPrice;
            set => SetProperty(ref _productPrice, value);
        }

        public ICommand AddNewCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public async Task LoadData()
        {
            var result = await _apiClient.List();

            if (result.HasErrors)
            {
                _dialogProvider.ShowError(result.GetErrorMessage());
                return;
            }

            Products.Clear();

            foreach (var product in result.Value ?? new())
            {
                Products.Add(product);
            }
        }

        public void AddNew()
        {
            SelectedProduct = null;
            SetSelection(null);
        }

        public async Task Save()
        {
            var result = await _apiClient.Save(new Product
            {
                Id = ProductId,
                Name = ProductName,
                Description = ProductDescription,
                Price = ProductPrice
            });

            if (result.HasErrors)
            {
                _dialogProvider.ShowError(result.GetErrorMessage());
                return;
            }

            await LoadData();
        }

        public async Task Delete()
        {
            if (ProductId <= 0)
            {
                return;
            }

            if (!_dialogProvider.ConfirmDelete())
            {
                return;
            }

            var result = await _apiClient.Delete(ProductId);

            if (result.HasErrors)
            {
                _dialogProvider.ShowError(result.GetErrorMessage());
                return;
            }

            AddNew();
            await LoadData();
        }

        private void SetSelection(Product? product)
        {
            ProductId = product?.Id ?? 0;
            ProductName = product?.Name ?? string.Empty;
            ProductDescription = product?.Description ?? string.Empty;
            ProductPrice = product?.Price ?? 0;
        }
    }
}
