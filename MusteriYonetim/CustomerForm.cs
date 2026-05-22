using MusteriYonetim.Models;

namespace MusteriYonetim;

public partial class CustomerForm : Form
{
    public Customer Musteri { get; private set; }

    public CustomerForm()
    {
        InitializeComponent();
        Musteri = new Customer();
        Text = "Yeni Müşteri Kartı";
    }

    public CustomerForm(Customer mevcut) : this()
    {
        Musteri = new Customer
        {
            CustomerId = mevcut.CustomerId,
            Name = mevcut.Name,
            Surname = mevcut.Surname,
            Phone = mevcut.Phone,
            Address = mevcut.Address,
            Balance = mevcut.Balance
        };
        Text = "Müşteri Düzenle";
        txtAd.Text = Musteri.Name;
        txtSoyad.Text = Musteri.Surname;
        txtTelefon.Text = Musteri.Phone ?? "";
        txtAdres.Text = Musteri.Address ?? "";
        txtBakiye.Text = Musteri.Balance.ToString();
    }

    private void btnKaydet_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtAd.Text) || string.IsNullOrWhiteSpace(txtSoyad.Text))
        {
            MessageBox.Show("Ad ve soyad zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!int.TryParse(txtBakiye.Text, out int bakiye))
        {
            MessageBox.Show("Bakiye tam sayı olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Musteri.Name = txtAd.Text.Trim();
        Musteri.Surname = txtSoyad.Text.Trim();
        Musteri.Phone = string.IsNullOrWhiteSpace(txtTelefon.Text) ? null : txtTelefon.Text.Trim();
        Musteri.Address = string.IsNullOrWhiteSpace(txtAdres.Text) ? null : txtAdres.Text.Trim();
        Musteri.Balance = bakiye;

        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnIptal_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
