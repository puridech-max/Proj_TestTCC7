using ProductCodeApp.Models;

namespace ProductCodeApp;

public partial class Form1 : Form
{
    private const int CenterMaxWidth = 1080;

    private bool _updatingPluInput;

    public Form1()
    {
        InitializeComponent();
        txtProductCode.TextChanged += TxtProductCode_TextChanged;
        txtProductCode.KeyPress += TxtProductCode_KeyPress;
        txtProductCode.KeyDown += TxtProductCode_KeyDown;
        Load += Form1_Load;
        Resize += (_, _) => LayoutCenterPanel();
        panelContentHost.Resize += (_, _) => LayoutCenterPanel();
        panelInputArea.Resize += (_, _) => LayoutInputRow();
    }

    private void TxtProductCode_TextChanged(object? sender, EventArgs e)
    {
        if (_updatingPluInput)
        {
            return;
        }

        var text = txtProductCode.Text;
        var caret = txtProductCode.SelectionStart;
        var alphanumericBeforeCaret = ProductCodeInputHelper.CountAlphanumeric(
            text[..Math.Min(caret, text.Length)]);

        var formatted = ProductCodeInputHelper.FormatForDisplay(text);
        if (formatted == text)
        {
            return;
        }

        _updatingPluInput = true;
        txtProductCode.Text = formatted;
        txtProductCode.SelectionStart = ProductCodeInputHelper.GetCaretIndex(
            formatted,
            Math.Min(alphanumericBeforeCaret, ProductCodeInputHelper.MaxAlphanumericLength));
        _updatingPluInput = false;
    }

    private void TxtProductCode_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.SuppressKeyPress = true;
        e.Handled = true;
        btnAdd.PerformClick();
    }

    private void TxtProductCode_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (char.IsControl(e.KeyChar))
        {
            return;
        }

        if (!char.IsLetterOrDigit(e.KeyChar))
        {
            e.Handled = true;
            return;
        }

        var start = txtProductCode.SelectionStart;
        var len = txtProductCode.SelectionLength;
        var withoutSelection = txtProductCode.Text.Remove(start, len);
        var afterType = ProductCodeInputHelper.CountAlphanumeric(withoutSelection) + 1;
        if (afterType > ProductCodeInputHelper.MaxAlphanumericLength)
        {
            e.Handled = true;
        }
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        LayoutCenterPanel();
        LayoutInputRow();
        _ = LoadGridAsync();
    }

    private void LayoutCenterPanel()
    {
        var marginH = 28;
        var marginV = 20;
        var w = Math.Min(CenterMaxWidth, Math.Max(640, panelContentHost.ClientSize.Width - marginH * 2));
        var h = Math.Max(360, panelContentHost.ClientSize.Height - marginV * 2);
        panelCenter.SetBounds(
            (panelContentHost.ClientSize.Width - w) / 2,
            marginV,
            w,
            h);
    }

    private void LayoutInputRow()
    {
        var pad = 0;
        var innerW = panelInputArea.ClientSize.Width - pad * 2;
        btnAdd.Left = innerW - btnAdd.Width;
        btnAdd.Top = 28;
        txtProductCode.SetBounds(pad, 28, Math.Max(200, btnAdd.Left - 12), txtProductCode.Height);
    }

    private async Task LoadGridAsync()
    {
        try
        {
            await RefreshGridAsync().ConfigureAwait(true);
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show(this,
                "เชื่อมต่อ API ไม่ได้ กรุณาเปิดโปรเจกต์ ProductCodeApi ให้รันอยู่ และตรวจสอบ ApiBaseUrl ใน appsettings.json\n\n" +
                ex.Message,
                "การเชื่อมต่อล้มเหลว",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            MessageBox.Show(this, "หมดเวลารอ API", "การเชื่อมต่อล้มเหลว", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async void BtnAdd_Click(object? sender, EventArgs e)
    {
        if (!ProductCodeValidator.TryNormalize(txtProductCode.Text, out var code, out var err))
        {
            MessageBox.Show(this, err, "รหัสสินค้าไม่ถูกต้อง", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var (ok, apiErr) = await Program.Api.CreateAsync(code).ConfigureAwait(true);
            if (!ok)
            {
                MessageBox.Show(this, apiErr ?? "ไม่สามารถบันทึกได้", "แจ้งเตือน",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtProductCode.Clear();
            txtProductCode.Focus();
            await RefreshGridAsync().ConfigureAwait(true);
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show(this, ex.Message, "เชื่อมต่อ API ไม่ได้", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            MessageBox.Show(this, "หมดเวลารอ API", "การเชื่อมต่อล้มเหลว", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async Task RefreshGridAsync()
    {
        var data = await Program.Api.GetProductsAsync().ConfigureAwait(true);
        gridProducts.DataSource = data.ToList();
    }

    private async void GridProducts_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        if (gridProducts.Columns[e.ColumnIndex] is not DataGridViewButtonColumn)
        {
            return;
        }

        var colName = gridProducts.Columns[e.ColumnIndex].Name;
        var idObj = gridProducts.Rows[e.RowIndex].Cells["colId"].Value;
        var codeObj = gridProducts.Rows[e.RowIndex].Cells["colPluCode"].Value;
        if (idObj is not int id || codeObj is not string code)
        {
            return;
        }

        if (colName == "colQr")
        {
            using var qr = new QrCodeForm(code);
            OwnerEnabledDialog(() => qr.ShowDialog(this));
        }
        else if (colName == "colDelete")
        {
            using var confirm = new ConfirmDeleteForm(code);
            var result = DialogResult.Cancel;
            OwnerEnabledDialog(() => result = confirm.ShowDialog(this));
            if (result != DialogResult.OK)
            {
                return;
            }

            try
            {
                var (ok, apiErr) = await Program.Api.DeleteAsync(id).ConfigureAwait(true);
                if (!ok)
                {
                    MessageBox.Show(this, apiErr ?? "ลบไม่สำเร็จ", "แจ้งเตือน",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await RefreshGridAsync().ConfigureAwait(true);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(this, ex.Message, "เชื่อมต่อ API ไม่ได้", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(this, "หมดเวลารอ API", "การเชื่อมต่อล้มเหลว", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }

    private void OwnerEnabledDialog(Action showModal)
    {
        Enabled = false;
        try
        {
            showModal();
        }
        finally
        {
            Enabled = true;
        }
    }
}
