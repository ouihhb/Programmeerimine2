using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms
{
    public partial class Form1 : Form, IProductsView
    {
        private readonly ProductPresenter _presenter;

        public Form1()
        {
            InitializeComponent();

            _presenter = new ProductPresenter(this, new ApiClient());
        }

        public object? DataSource
        {
            get => productsGrid.DataSource;
            set => productsGrid.DataSource = value;
        }

        public int ProductId
        {
            get => int.TryParse(idTextBox.Text, out var id) ? id : 0;
            set => idTextBox.Text = value == 0 ? string.Empty : value.ToString();
        }

        public new string ProductName
        {
            get => nameTextBox.Text;
            set => nameTextBox.Text = value;
        }

        public string ProductDescription
        {
            get => descriptionTextBox.Text;
            set => descriptionTextBox.Text = value;
        }

        public decimal ProductPrice
        {
            get => decimal.TryParse(priceTextBox.Text, out var price) ? price : 0;
            set => priceTextBox.Text = value == 0 ? string.Empty : value.ToString("0.##");
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool ConfirmDelete()
        {
            return MessageBox.Show(
                "Kas oled kindel, et soovid toote kustutada?",
                "Kinnita kustutamine",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await _presenter.LoadData();
        }

        private void ProductsGrid_SelectionChanged(object sender, EventArgs e)
        {
            var product = productsGrid.CurrentRow?.DataBoundItem as Product;
            _presenter.SetSelection(product);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            _presenter.AddNew();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            await _presenter.Save();
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            await _presenter.Delete();
        }
    }
}
