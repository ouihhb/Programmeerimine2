namespace KooliProjekt.WindowsForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            productsGrid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)productsGrid).BeginInit();
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
            productsGrid.Name = "productsGrid";
            productsGrid.ReadOnly = true;
            productsGrid.RowHeadersWidth = 51;
            productsGrid.Size = new Size(800, 450);
            productsGrid.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(productsGrid);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)productsGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView productsGrid;
    }
}
