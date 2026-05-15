using QRCoder;

namespace ProductCodeApp;

public sealed class QrCodeForm : Form
{
    private static readonly Color Blue = Color.FromArgb(25, 118, 210);

    public QrCodeForm(string productCode)
    {
        Text = "QR Code";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(420, 480);
        BackColor = Color.White;

        var picture = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(20, 20),
            Size = new Size(380, 380),
            BorderStyle = BorderStyle.FixedSingle
        };

        using (var gen = new QRCodeGenerator())
        using (var data = gen.CreateQrCode(productCode, QRCodeGenerator.ECCLevel.M))
        using (var qr = new QRCode(data))
        using (var bmp = qr.GetGraphic(pixelsPerModule: 12))
        {
            picture.Image = new Bitmap(bmp);
        }

        var closeBtn = new Button
        {
            Text = "Close",
            DialogResult = DialogResult.OK,
            BackColor = Blue,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            AutoSize = true,
            Padding = new Padding(16, 6, 16, 6),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right
        };
        closeBtn.FlatAppearance.BorderSize = 0;

        Controls.Add(picture);
        Controls.Add(closeBtn);
        AcceptButton = closeBtn;

        void LayoutClose(object? s, EventArgs e)
        {
            closeBtn.Location = new Point(ClientSize.Width - closeBtn.Width - 20,
                ClientSize.Height - closeBtn.Height - 16);
        }

        Layout += LayoutClose;
        Shown += (_, _) => LayoutClose(null, EventArgs.Empty);

        FormClosed += (_, _) => picture.Image?.Dispose();
    }
}
