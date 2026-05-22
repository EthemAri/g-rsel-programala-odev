-- Customer tablosu (SQLite)
-- Uygulama ilk çalıştırmada bunu otomatik oluşturur.
-- Bu dosya ön hazırlık / ders raporu için referanstır.

CREATE TABLE IF NOT EXISTS Customer (
    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name       TEXT NOT NULL,
    Surname    TEXT NOT NULL,
    Phone      TEXT,
    Address    TEXT,
    Balance    INTEGER NOT NULL DEFAULT 0
);
