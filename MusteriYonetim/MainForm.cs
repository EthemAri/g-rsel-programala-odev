using MusteriYonetim.Data;
using MusteriYonetim.Models;

namespace MusteriYonetim;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        ListeyiYukle();
    }

    private void ListeyiYukle()
    {
        try
        {
            var liste = DatabaseHelper.GetAll();
            dgvListe.DataSource = liste.Select(m => new
            {
                m.CustomerId,
                Ad = m.Name,
                Soyad = m.Surname,
                Telefon = m.Phone ?? "",
                Adres = m.Address ?? "",
                m.Balance
            }).ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Veritabanı hatası:\n\n" + ex.Message,
                "Veritabanı Hatası",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private int? SeciliId()
    {
        if (dgvListe.CurrentRow == null) return null;
        return Convert.ToInt32(dgvListe.CurrentRow.Cells["CustomerId"].Value);
    }

    private void btnYeni_Click(object sender, EventArgs e)
    {
        using var form = new CustomerForm();
        if (form.ShowDialog() != DialogResult.OK) return;

        try
        {
            DatabaseHelper.Insert(form.Musteri);
            ListeyiYukle();
            MessageBox.Show("Müşteri eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnDuzenle_Click(object sender, EventArgs e)
    {
        var id = SeciliId();
        if (id == null)
        {
            MessageBox.Show("Lütfen bir müşteri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var mevcut = DatabaseHelper.GetById(id.Value);
        if (mevcut == null) return;

        using var form = new CustomerForm(mevcut);
        if (form.ShowDialog() != DialogResult.OK) return;

        try
        {
            DatabaseHelper.Update(form.Musteri);
            ListeyiYukle();
            MessageBox.Show("Müşteri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnSil_Click(object sender, EventArgs e)
    {
        var id = SeciliId();
        if (id == null)
        {
            MessageBox.Show("Lütfen bir müşteri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (MessageBox.Show("Bu müşteriyi silmek istiyor musunuz?", "Silme Onayı",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            DatabaseHelper.Delete(id.Value);
            ListeyiYukle();
            MessageBox.Show("Müşteri silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnYenile_Click(object sender, EventArgs e) => ListeyiYukle();
}
