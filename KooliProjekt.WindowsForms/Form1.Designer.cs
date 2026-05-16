namespace KooliProjekt.WindowsForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            productsGrid = new DataGridView();
            editPanel = new Panel();
            buttonsPanel = new FlowLayoutPanel();
            addButton = new Button();
            saveButton = new Button();
            deleteButton = new Button();
            priceTextBox = new TextBox();
            priceLabel = new Label();
            descriptionTextBox = new TextBox();
            descriptionLabel = new Label();
            nameTextBox = new TextBox();
            nameLabel = new Label();
            idTextBox = new TextBox();
            idLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)productsGrid).BeginInit();
            editPanel.SuspendLayout();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // productsGrid
            // 
            productsGrid.AllowUserToAddRows = false;
            productsGrid.AllowUserToDeleteRows = false;
            productsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            productsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            productsGrid.Dock = DockStyle.Fill;
            productsGrid.Location = new Point(0, 0);
            productsGrid.MultiSelect = false;
            productsGrid.Name = "productsGrid";
            productsGrid.ReadOnly = true;
            productsGrid.RowHeadersWidth = 51;
            productsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            productsGrid.Size = new Size(552, 450);
            productsGrid.TabIndex = 0;
            productsGrid.SelectionChanged += ProductsGrid_SelectionChanged;
            // 
            // editPanel
            // 
            editPanel.Controls.Add(buttonsPanel);
            editPanel.Controls.Add(priceTextBox);
            editPanel.Controls.Add(priceLabel);
            editPanel.Controls.Add(descriptionTextBox);
            editPanel.Controls.Add(descriptionLabel);
            editPanel.Controls.Add(nameTextBox);
            editPanel.Controls.Add(nameLabel);
            editPanel.Controls.Add(idTextBox);
            editPanel.Controls.Add(idLabel);
            editPanel.Dock = DockStyle.Right;
            editPanel.Location = new Point(552, 0);
            editPanel.Name = "editPanel";
            editPanel.Padding = new Padding(16);
            editPanel.Size = new Size(248, 450);
            editPanel.TabIndex = 1;
            // 
            // buttonsPanel
            // 
            buttonsPanel.Controls.Add(addButton);
            buttonsPanel.Controls.Add(saveButton);
            buttonsPanel.Controls.Add(deleteButton);
            buttonsPanel.Location = new Point(16, 266);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new Size(216, 84);
            buttonsPanel.TabIndex = 8;
            // 
            // addButton
            // 
            addButton.Location = new Point(3, 3);
            addButton.Name = "addButton";
            addButton.Size = new Size(94, 29);
            addButton.TabIndex = 0;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += AddButton_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(103, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(94, 29);
            saveButton.TabIndex = 1;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(3, 38);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(94, 29);
            deleteButton.TabIndex = 2;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += DeleteButton_Click;
            // 
            // priceTextBox
            // 
            priceTextBox.Location = new Point(16, 221);
            priceTextBox.Name = "priceTextBox";
            priceTextBox.Size = new Size(216, 27);
            priceTextBox.TabIndex = 7;
            // 
            // priceLabel
            // 
            priceLabel.AutoSize = true;
            priceLabel.Location = new Point(16, 198);
            priceLabel.Name = "priceLabel";
            priceLabel.Size = new Size(41, 20);
            priceLabel.TabIndex = 6;
            priceLabel.Text = "Price";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Location = new Point(16, 159);
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.Size = new Size(216, 27);
            descriptionTextBox.TabIndex = 5;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(16, 136);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(85, 20);
            descriptionLabel.TabIndex = 4;
            descriptionLabel.Text = "Description";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(16, 97);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(216, 27);
            nameTextBox.TabIndex = 3;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(16, 74);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(49, 20);
            nameLabel.TabIndex = 2;
            nameLabel.Text = "Name";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(16, 35);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(216, 27);
            idTextBox.TabIndex = 1;
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(16, 12);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(24, 20);
            idLabel.TabIndex = 0;
            idLabel.Text = "Id";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(productsGrid);
            Controls.Add(editPanel);
            Name = "Form1";
            Text = "Products";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)productsGrid).EndInit();
            editPanel.ResumeLayout(false);
            editPanel.PerformLayout();
            buttonsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private DataGridView productsGrid;
        private Panel editPanel;
        private TextBox idTextBox;
        private Label idLabel;
        private TextBox nameTextBox;
        private Label nameLabel;
        private TextBox descriptionTextBox;
        private Label descriptionLabel;
        private TextBox priceTextBox;
        private Label priceLabel;
        private FlowLayoutPanel buttonsPanel;
        private Button addButton;
        private Button saveButton;
        private Button deleteButton;
    }
}
