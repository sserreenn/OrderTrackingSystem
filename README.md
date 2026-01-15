📦 Order Tracking System (Sipariş Takip Sistemi)
Bu proje, bir sipariş yönetim sisteminin gereksinimlerini karşılamak amacıyla Clean Architecture prensipleriyle geliştirilmiş, sağlam ve ölçeklenebilir bir ASP.NET Core Web API uygulamasıdır.

Proje; iş kurallarının (Business Rules) merkezi bir noktada toplandığı, veritabanı işlemlerinin soyutlandığı ve performans optimizasyonlarının yapıldığı modern bir mimariyi temsil eder.

🛠️ Mimari ve Teknolojiler
.NET 8.0 & C# 12

Clean Architecture (N-Tier): Core, DataAccess, Business ve API katmanları.

Entity Framework Core: Veritabanı yönetim sistemi.

Repository & Unit of Work Patterns: Veri erişim katmanının soyutlanması ve atomik işlem yönetimi.

AutoMapper: Entity-DTO dönüşümleri.

FluentValidation: Giriş verilerinin ve iş kurallarının doğrulanması.

In-Memory Caching: Sık kullanılan veriler (Müşteri listesi vb.) için performans artırımı.

Global Exception Handling: Merkezi hata yönetimi middleware'i.

🎯 Uygulanan İş Kuralları (Business Rules)
Sipariş Sınırı: Bir müşteri aynı gün içerisinde en fazla 5 adet sipariş oluşturabilir.

Statü Kontrolü: Cancelled (İptal) veya Completed (Tamamlandı) durumuna geçmiş bir siparişin statüsü bir daha değiştirilemez.

Otomatik Hesaplama: Sipariş toplam tutarı (TotalAmount), API tüketicisinden alınmaz; sunucu tarafında sipariş kalemlerinin (miktar * birim fiyat) toplamı üzerinden otomatik olarak hesaplanarak veri bütünlüğü sağlanır.

🚀 Kurulum ve Çalıştırma
1. Veritabanı Yapılandırması
OrderTracking.API/appsettings.json dosyasındaki DefaultConnection bağlantı cümlesini yerel SQL Server ayarlarınıza göre düzenleyin:

JSON

"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=OrderTrackingDb;Trusted_Connection=True;TrustServerCertificate=True"
2. Migration Uygulama
Paket Yöneticisi Konsolu'nu (Package Manager Console) açın ve DataAccess projesini seçerek şu komutu çalıştırın:

PowerShell

Update-Database
3. Uygulamayı Başlatma
Proje ayağa kalktığında Swagger UI (/swagger) otomatik olarak yüklenecektir.

🏗️ Proje Katmanları
Core: Entity'ler, Enum'lar, DTO'lar ve Repository interface'leri.

DataAccess: DB Context, Entity yapılandırmaları ve Repository implementasyonları.

Business: İş mantığı servisleri, Mapping profilleri ve Validasyon kuralları.

API: Controller'lar, Middleware yapılandırmaları ve Dependency Injection tanımları.

⚡ Performans ve Hata Yönetimi
Caching: GetAllCustomers ve GetOrdersWithPagination gibi yoğun kullanılan metotlarda IMemoryCache kullanılarak veritabanı yükü optimize edilmiştir.

Middleware: Uygulama içerisinde fırlatılan tüm hatalar GlobalExceptionMiddleware tarafından yakalanarak, istemciye standart bir JSON formatında (StatusCode ve Message) iletilir.
