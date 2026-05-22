using Microsoft.Data.Sqlite;
using MusteriYonetim.Models;

namespace MusteriYonetim.Data;

public static class DatabaseHelper
{
    private static string? _connectionString;

    public static string ConnectionString =>
        _connectionString ??= $"Data Source={VeritabaniDosyaYolu()}";

    /// <summary>
    /// İlk çalıştırmada veritabanı dosyası, tablo ve örnek kayıtlar otomatik oluşur.
    /// </summary>
    public static void Initialize()
    {
        var dbPath = VeritabaniDosyaYolu();
        var klasor = Path.GetDirectoryName(dbPath)!;
        if (!Directory.Exists(klasor))
            Directory.CreateDirectory(klasor);

        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();

        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = """
                CREATE TABLE IF NOT EXISTS Customer (
                    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name       TEXT NOT NULL,
                    Surname    TEXT NOT NULL,
                    Phone      TEXT,
                    Address    TEXT,
                    Balance    INTEGER NOT NULL DEFAULT 0
                );
                """;
            cmd.ExecuteNonQuery();
        }

        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = "SELECT COUNT(*) FROM Customer";
            var count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count > 0) return;
        }

        using (var cmd = conn.CreateCommand())
        {
            cmd.CommandText = """
                INSERT INTO Customer (Name, Surname, Phone, Address, Balance) VALUES
                ('Ahmet', 'Yılmaz', '0532 111 2233', 'İstanbul, Kadıköy', 1500),
                ('Ayşe', 'Demir', '0544 555 6677', 'Ankara, Çankaya', 0);
                """;
            cmd.ExecuteNonQuery();
        }
    }

    private static string VeritabaniDosyaYolu()
    {
        var dir = AppDomain.CurrentDomain.BaseDirectory;
        for (var i = 0; i < 10; i++)
        {
            if (File.Exists(Path.Combine(dir, "MusteriYonetim.csproj")))
                return Path.GetFullPath(Path.Combine(dir, "..", "Database", "MusteriYonetim.db"));

            var parent = Directory.GetParent(dir);
            if (parent == null) break;
            dir = parent.FullName;
        }

        return Path.GetFullPath(Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..",
            "Database", "MusteriYonetim.db"));
    }

    public static List<Customer> GetAll()
    {
        var list = new List<Customer>();
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            SELECT CustomerId, Name, Surname, Phone, Address, Balance
            FROM Customer ORDER BY CustomerId
            """;
        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add(ReadCustomer(r));
        return list;
    }

    public static Customer? GetById(int id)
    {
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            SELECT CustomerId, Name, Surname, Phone, Address, Balance
            FROM Customer WHERE CustomerId = @Id
            """;
        cmd.Parameters.AddWithValue("@Id", id);
        using var r = cmd.ExecuteReader();
        return r.Read() ? ReadCustomer(r) : null;
    }

    public static int Insert(Customer c)
    {
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            INSERT INTO Customer (Name, Surname, Phone, Address, Balance)
            VALUES (@Name, @Surname, @Phone, @Address, @Balance);
            SELECT last_insert_rowid();
            """;
        AddParams(cmd, c);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public static void Update(Customer c)
    {
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            UPDATE Customer
            SET Name = @Name, Surname = @Surname, Phone = @Phone,
                Address = @Address, Balance = @Balance
            WHERE CustomerId = @CustomerId
            """;
        cmd.Parameters.AddWithValue("@CustomerId", c.CustomerId);
        AddParams(cmd, c);
        cmd.ExecuteNonQuery();
    }

    public static void Delete(int id)
    {
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM Customer WHERE CustomerId = @Id";
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.ExecuteNonQuery();
    }

    private static void AddParams(SqliteCommand cmd, Customer c)
    {
        cmd.Parameters.AddWithValue("@Name", c.Name.Trim());
        cmd.Parameters.AddWithValue("@Surname", c.Surname.Trim());
        cmd.Parameters.AddWithValue("@Phone", (object?)c.Phone ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Address", (object?)c.Address ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Balance", c.Balance);
    }

    private static Customer ReadCustomer(SqliteDataReader r) => new()
    {
        CustomerId = r.GetInt32(0),
        Name = r.GetString(1),
        Surname = r.GetString(2),
        Phone = r.IsDBNull(3) ? null : r.GetString(3),
        Address = r.IsDBNull(4) ? null : r.GetString(4),
        Balance = r.GetInt32(5)
    };
}
