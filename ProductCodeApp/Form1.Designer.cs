namespace ProductCodeApp;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    private Panel panelHeader = null!;
    private Label lblHeaderTitle = null!;
    private Panel panelContentHost = null!;
    private Panel panelCenter = null!;
    private Panel panelInputArea = null!;
    private Label lblProductCode = null!;
    private TextBox txtProductCode = null!;
    private Button btnAdd = null!;
    private DataGridView gridProducts = null!;

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
        panelHeader = new Panel();
        lblHeaderTitle = new Label();
        panelContentHost = new Panel();
        panelCenter = new Panel();
        panelInputArea = new Panel();
        lblProductCode = new Label();
        txtProductCode = new TextBox();
        btnAdd = new Button();
        gridProducts = new DataGridView();
        panelHeader.SuspendLayout();
        panelContentHost.SuspendLayout();
        panelCenter.SuspendLayout();
        panelInputArea.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridProducts).BeginInit();
        SuspendLayout();

        var green = Color.FromArgb(46, 125, 50);
        var blue = Color.FromArgb(25, 118, 210);

        panelHeader.BackColor = green;
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Height = 52;
        panelHeader.Controls.Add(lblHeaderTitle);

        lblHeaderTitle.AutoSize = true;
        lblHeaderTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblHeaderTitle.ForeColor = Color.White;
        lblHeaderTitle.Location = new Point(16, 14);
        lblHeaderTitle.Text = "IT 07 — จัดการรหัสสินค้า";

        panelContentHost.BackColor = SystemColors.ControlLight;
        panelContentHost.Dock = DockStyle.Fill;

        panelCenter.BackColor = Color.White;
        panelCenter.Padding = new Padding(12);

        panelInputArea.BackColor = Color.White;
        panelInputArea.Dock = DockStyle.Top;
        panelInputArea.Height = 112;

        lblProductCode.AutoSize = true;
        lblProductCode.Font = new Font("Segoe UI", 10F);
        lblProductCode.Location = new Point(0, 0);
        lblProductCode.Text = "รหัสสินค้า";

        txtProductCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtProductCode.Font = new Font("Segoe UI", 10F);
        txtProductCode.Location = new Point(0, 28);
        txtProductCode.Height = 28;
        txtProductCode.MaxLength = 35;
        txtProductCode.PlaceholderText = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX";
        txtProductCode.CharacterCasing = CharacterCasing.Upper;

        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.BackColor = blue;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.Cursor = Cursors.Hand;
        btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnAdd.ForeColor = Color.White;
        btnAdd.Size = new Size(120, 34);
        btnAdd.Text = "ADD";
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += BtnAdd_Click;

        panelInputArea.Controls.Add(btnAdd);
        panelInputArea.Controls.Add(txtProductCode);
        panelInputArea.Controls.Add(lblProductCode);

        gridProducts.AllowUserToAddRows = false;
        gridProducts.AllowUserToDeleteRows = false;
        gridProducts.AutoGenerateColumns = false;
        gridProducts.BackgroundColor = Color.White;
        gridProducts.BorderStyle = BorderStyle.Fixed3D;
        gridProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridProducts.Dock = DockStyle.Fill;
        gridProducts.ReadOnly = false;
        gridProducts.RowHeadersVisible = false;
        gridProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridProducts.MultiSelect = false;
        gridProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridProducts.CellContentClick += GridProducts_CellContentClick;
        gridProducts.MinimumSize = new Size(600, 280);

        var colId = new DataGridViewTextBoxColumn
        {
            Name = "colId",
            DataPropertyName = "Id",
            HeaderText = "Id",
            FillWeight = 12,
            MinimumWidth = 56,
            ReadOnly = true
        };
        var colCode = new DataGridViewTextBoxColumn
        {
            Name = "colPluCode",
            DataPropertyName = "PluCode",
            HeaderText = "รหัสสินค้า",
            FillWeight = 58,
            MinimumWidth = 260,
            ReadOnly = true
        };
        var colQr = new DataGridViewButtonColumn
        {
            Name = "colQr",
            HeaderText = "View",
            Text = "QR",
            UseColumnTextForButtonValue = true,
            FillWeight = 15,
            MinimumWidth = 80
        };
        colQr.DefaultCellStyle.BackColor = Color.FromArgb(67, 160, 71);
        colQr.DefaultCellStyle.ForeColor = Color.White;
        colQr.DefaultCellStyle.SelectionBackColor = colQr.DefaultCellStyle.BackColor;
        colQr.DefaultCellStyle.SelectionForeColor = Color.White;

        var colDel = new DataGridViewButtonColumn
        {
            Name = "colDelete",
            HeaderText = "Delete",
            Text = "ลบ",
            UseColumnTextForButtonValue = true,
            FillWeight = 15,
            MinimumWidth = 80
        };
        colDel.DefaultCellStyle.BackColor = Color.FromArgb(211, 47, 47);
        colDel.DefaultCellStyle.ForeColor = Color.White;
        colDel.DefaultCellStyle.SelectionBackColor = colDel.DefaultCellStyle.BackColor;
        colDel.DefaultCellStyle.SelectionForeColor = Color.White;

        gridProducts.Columns.AddRange(colId, colCode, colQr, colDel);

        panelCenter.Controls.Add(gridProducts);
        panelCenter.Controls.Add(panelInputArea);
        panelContentHost.Controls.Add(panelCenter);
        Controls.Add(panelContentHost);
        Controls.Add(panelHeader);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 640);
        MinimumSize = new Size(900, 520);
        Text = "Product Code Manager";
        StartPosition = FormStartPosition.CenterScreen;

        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        panelInputArea.ResumeLayout(false);
        panelInputArea.PerformLayout();
        panelCenter.ResumeLayout(false);
        panelCenter.PerformLayout();
        panelContentHost.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridProducts).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
