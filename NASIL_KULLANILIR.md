# Müşteri Yönetim Sistemi — Nasıl Kullanılır

**Ders:** Görsel Programlama  
**Teknoloji:** C# (Windows Forms), SQLite  
**Tarih:** 22.05.2026

---

## 1. Proje ne yapar?

Müşteri bilgilerini **SQLite veritabanında** saklayan masaüstü uygulamadır. Uygulama açıldığında veritabanı ve `Customer` tablosu **otomatik oluşur**; ayrı kurulum gerekmez.

## 2. Hangi problemi çözer?

- Müşteri kayıtlarının dağınık tutulmasından kaynaklanan kayıp ve karışıklığı azaltır.
- Bakiye ve iletişim bilgilerinin tek yerden yönetilmesini sağlar.
- Ekleme / silme / güncelleme işlemlerini standart formlar üzerinden yapar.

## 3. Gereksinimler

| Kod | Gereksinim |
|-----|------------|
| FR-01 | `Customer` tablosu oluşturulmalı |
| FR-02 | Yeni müşteri eklenebilmeli |
| FR-03 | Müşteriler listelenebilmeli |
| FR-04 | Seçili müşteri düzenlenebilmeli |
| FR-05 | Seçili müşteri silinebilmeli |
| FR-06 | Ad ve soyad zorunlu olmalı |
| FR-07 | Bakiye tam sayı (integer) olmalı |

**Teknik:** SQLite, C# WinForms, ADO.NET (`SqliteConnection`)

---

## 4. Veritabanı şeması

### ER diyagramı

```
┌─────────────────────────────────────┐
│            Customer                 │
├─────────────────────────────────────┤
│ PK  CustomerId    INTEGER AUTOINC   │
│     Name          TEXT              │
│     Surname       TEXT              │
│     Phone         TEXT              │
│     Address       TEXT              │
│     Balance       INTEGER           │
└─────────────────────────────────────┘
```

### Tablo: Customer

| Sütun | Tip | Null | Açıklama |
|-------|-----|------|----------|
| CustomerId | INTEGER | Hayır | Birincil anahtar, otomatik artan |
| Name | TEXT | Hayır | Ad |
| Surname | TEXT | Hayır | Soyad |
| Phone | TEXT | Evet | Telefon |
| Address | TEXT | Evet | Adres |
| Balance | INTEGER | Hayır | Bakiye (varsayılan 0) |

**Dosya:** `Database\MusteriYonetim.db` (uygulama ilk çalıştırmada oluşturur)

Referans SQL: `Database\01_CustomerTablosu.sql`

---

## 5. Ekran tasarımları

### Ana form

```
┌────────────────────────────────────────────────────────────┐
│  Müşteri Yönetim Sistemi                          [_][□][X] │
├────────────────────────────────────────────────────────────┤
│  [ Yeni ]  [ Düzenle ]  [ Sil ]  [ Yenile ]                │
├────────────────────────────────────────────────────────────┤
│  ID │ Ad    │ Soyad  │ Telefon    │ Adres      │ Bakiye   │
└────────────────────────────────────────────────────────────┘
```

### Müşteri kartı

```
┌──────────────────────────────────┐
│  Ad *       [________________]   │
│  Soyad *    [________________]   │
│  Telefon    [________________]   │
│  Adres      [________________]   │
│  Bakiye *   [________]           │
│     [ Kaydet ]    [ İptal ]      │
└──────────────────────────────────┘
```

---

## 6. Mimari

```
[ Windows Forms ]  ←→  [ ADO.NET ]  ←→  [ SQLite ]
 MainForm              SqliteConnection    MusteriYonetim.db
 CustomerForm                               Customer
```

---

## 7. Uygulamayı çalıştırma

**Visual Studio:** `MusteriYonetim\MusteriYonetim.csproj` açın → **F5**

**Terminal:**

```powershell
cd "c:\Users\ibrah\OneDrive\Masaüstü\görsel programlama\MusteriYonetim"
dotnet run
```

| Buton | İşlev |
|-------|--------|
| Yeni | Müşteri ekle |
| Düzenle | Seçili kaydı güncelle |
| Sil | Seçili kaydı sil |
| Yenile | Listeyi yenile |

---

## 8. Test senaryoları

1. Uygulamayı çalıştır → liste ve örnek kayıtlar görünmeli.
2. Yeni müşteri ekle → kayıt listede olmalı.
3. Düzenle → değişiklik kalıcı olmalı.
4. Sil → kayıt kalkmalı.
