namespace ProductCodeApp;

public sealed class ConfirmDeleteForm : Form
{
    private static readonly Color Blue = Color.FromArgb(25, 118, 210);
    private static readonly Color Red = Color.FromArgb(211, 47, 47);

    public ConfirmDeleteForm(string productCode)
    {
        Text = "ยืนยันการลบ";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(520, 160);
        BackColor = Color.White;

        var message = new Label
        {
            AutoSize = false,
            Text = $"ต้องการลบข้อมูล รหัสสินค้า {productCode} หรือไม่ ?",
            Font = new Font("Segoe UI", 10F),
            Location = new Point(20, 24),
            Size = new Size(480, 60)
        };

        var ok = new Button
        {
            Text = "ตกลง",
            DialogResult = DialogResult.OK,
            BackColor = Blue,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            AutoSize = true,
            Padding = new Padding(20, 8, 20, 8),
            Location = new Point(280, 100)
        };
        ok.FlatAppearance.BorderSize = 0;

        var cancel = new Button
        {
            Text = "ยกเลิก",
            DialogResult = DialogResult.Cancel,
            BackColor = Red,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            AutoSize = true,
            Padding = new Padding(20, 8, 20, 8),
            Location = new Point(380, 100)
        };
        cancel.FlatAppearance.BorderSize = 0;

        Controls.Add(message);
        Controls.Add(ok);
        Controls.Add(cancel);
        AcceptButton = ok;
        CancelButton = cancel;
    }
}
