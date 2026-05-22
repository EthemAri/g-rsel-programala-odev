namespace MusteriYonetim;

partial class MainForm
{
    private DataGridView dgvListe;
    private Button btnYeni;
    private Button btnDuzenle;
    private Button btnSil;
    private Button btnYenile;

    private void InitializeComponent()
    {
        dgvListe = new DataGridView();
        btnYeni = new Button();
        btnDuzenle = new Button();
        btnSil = new Button();
        btnYenile = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvListe).BeginInit();
        SuspendLayout();

        btnYeni.Text = "Yeni";
        btnYeni.Location = new Point(12, 12);
        btnYeni.Size = new Size(90, 32);
        btnYeni.Click += btnYeni_Click;

        btnDuzenle.Text = "Düzenle";
        btnDuzenle.Location = new Point(108, 12);
        btnDuzenle.Size = new Size(90, 32);
        btnDuzenle.Click += btnDuzenle_Click;

        btnSil.Text = "Sil";
        btnSil.Location = new Point(204, 12);
        btnSil.Size = new Size(90, 32);
        btnSil.Click += btnSil_Click;

        btnYenile.Text = "Yenile";
        btnYenile.Location = new Point(300, 12);
        btnYenile.Size = new Size(90, 32);
        btnYenile.Click += btnYenile_Click;

        dgvListe.Location = new Point(12, 54);
        dgvListe.Size = new Size(860, 400);
        dgvListe.ReadOnly = true;
        dgvListe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvListe.MultiSelect = false;
        dgvListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvListe.AllowUserToAddRows = false;

        ClientSize = new Size(884, 471);
        Controls.AddRange(new Control[] { btnYeni, btnDuzenle, btnSil, btnYenile, dgvListe });
        Text = "Müşteri Yönetim Sistemi";
        StartPosition = FormStartPosition.CenterScreen;

        ((System.ComponentModel.ISupportInitialize)dgvListe).EndInit();
        ResumeLayout(false);
    }
}
