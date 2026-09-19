# BordoStock

ASP.NET Core MVC ve SQL Server tabanlı stok/ürün takip uygulaması.

## Çalıştırma

1. `BordoStock/appsettings.json` içindeki SQL Server bağlantısını bilgisayarına göre düzenle.
2. SQL Server'da `Database/BordoStockSchema.sql` dosyasını bir kez çalıştır veya EF migration kullan.
3. Terminalde:

```bash
cd BordoStock
dotnet restore
dotnet run
```

## Demo giriş

- E-posta: `admin@bordostock.com`
- Şifre: `admin123`

Uygulama; ürün, kategori, tedarikçi, depo ve stok giriş/çıkış işlemlerini içerir. Tema siyah-gri-bordo olarak tasarlanmıştır.
