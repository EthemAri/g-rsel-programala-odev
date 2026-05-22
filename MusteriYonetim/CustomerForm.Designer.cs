namespace MusteriYonetim;

partial class CustomerForm
{
    private Label lblAd, lblSoyad, lblTelefon, lblAdres, lblBakiye;
    private TextBox txtAd, txtSoyad, txtTelefon, txtAdres, txtBakiye;
    private Button btnKaydet, btnIptal;

    private void InitializeComponent()
    {
        lblAd = new Label { Text = "Ad:", Location = new Point(20, 22), AutoSize = true };
        txtAd = new TextBox { Location = new Point(100, 19), Size = new Size(260, 23) };

        lblSoyad = new Label { Text = "Soyad:", Location = new Point(20, 57), AutoSize = true };
        txtSoyad = new TextBox { Location = new Point(100, 54), Size = new Size(260, 23) };

        lblTelefon = new Label { Text = "Telefon:", Location = new Point(20, 92), AutoSize = true };
        txtTelefon = new TextBox { Location = new Point(100, 89), Size = new Size(260, 23) };

        lblAdres = new Label { Text = "Adres:", Location = new Point(20, 127), AutoSize = true };
        txtAdres = new TextBox { Location = new Point(100, 124), Size = new Size(260, 23) };

        lblBakiye = new Label { Text = "Bakiye:", Location = new Point(20, 162), AutoSize = true };
        txtBakiye = new TextBox { Location = new Point(100, 159), Size = new Size(120, 23) };

        btnKaydet = new Button { Text = "Kaydet", Location = new Point(100, 200), Size = new Size(90, 32) };
        btnKaydet.Click += btnKaydet_Click;

        btnIptal = new Button { Text = "İptal", Location = new Point(200, 200), Size = new Size(90, 32) };
        btnIptal.Click += btnIptal_Click;

        ClientSize = new Size(390, 250);
        Controls.AddRange(new Control[]
        {
            lblAd, txtAd, lblSoyad, txtSoyad, lblTelefon, txtTelefon,
            lblAdres, txtAdres, lblBakiye, txtBakiye, btnKaydet, btnIptal
        });
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Müşteri Kartı";
    }
}
