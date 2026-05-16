using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms
{
    public partial class Form1 : Form
    {
        private readonly IApiClient _apiClient;

        public Form1()
        {
            InitializeComponent();

            _apiClient = new ApiClient();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var products = await _apiClient.GetProducts();

                productsGrid.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Toodete laadimine ebaonnestus: {ex.Message}",
                    "Viga",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
