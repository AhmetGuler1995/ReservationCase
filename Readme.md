# Rezervasyon İşlemleri Örnek Solution

İlgili proje; bir kafe, restoran veya hizmet sektörü ile ilgili işlem yapan firmalarda rezervasyon durumlarında çözüm sağlamak amacı ile geliştirilmiştir. Proje **.Net** teknolojileri ile geliştirilmiş olup **onion (soğan) tasarım paternini** kullanmaktadır. Veritabanı süreçlerinde **EntityFramework** ORM bileşeni ile çalışıp UnitOfWork Yaklaşımı Uygulanmıştır. Test projelerinde ise **xUnit** ve **MsUnit** Test proje tiplerinde çalışılmıştır. Projede DepencyInjection durumları için Asp.Net Core 'da bulunan **DependencyInjection** kullanılmış olup dependency durumları minimal seviyelerde tutulmuştur.


# Katmanları
### Domain Katmanı
Domain katmanı ilgili projede **Entities**, **RequestDto classes**, **ResponseModels**, **ConfigurationModels** gibi çeşitli poco classları barındıran katman olarak görev almaktadır.

### Persistence Katmanı
Persistence katmanı projenin **EntityFramework** bileşenini kullanmasından dolayı gereksinim kazanmış bir katmandır. **Persistence Katmanı;** projenin içinde bulunan Entity modellerin değişmesi sonucu veritabanı migration operasyonlarının döndüğü katman olup aynı zamanda DbContext'i de içinde barındırmaktadır.
  
### Core Katmanı
Core katmanı aslında Onion patern incelendiğinde Repository ve Service katmanının birlikte kullanılmasına dayanmaktadır. Onion paternine göre referans alması gereken katman sadece **Domain Katmanıdır**.
##### *Repository Bileşeni*
Bu projede; repository bileşeninde GenericRepository yaklaşımı kullanılmıştır. Böylelikle klasikleşmiş sql süreçleri için her repository dosyasına benzer metotlar oluşturulmaması sağlanmıştır. Repository Katmanı **Sql Operation** süreçlerinden sorumlu olan bu bileşen Sadece ResponseModel ve **Entity**lerle iç içe olarak süreçlerini ve sorumluluklarını gerçekleştirir.
##### *Service Bileşeni*
Bu projede; servis bileşeninde **Business Logic (İş Mantıkları)** süreçleri dönmektedir. Onion Paternini  incelendiğinde **Service Bileşeni** olan herhangi bir sorgu çalıştırmaz ve DbContext 'e bağlanmaz onun yerine repositorylere bağlanarak  veritabanı **CRUD Operationları** tetiklemektedir. Bu yüzden repository ihtiyacının projede artması yığınlaşma(stack durumunun oluşması) yaklaşımlara sokmaması için GenericRepository yaklaşımı kullanılmıştır.
### Infrastructure Katmanı
Bu katman dışarıda servislere entegrasyonların gerçekleştiği katman olup, **Mail, Sms, Payment Systems** konularını bünyesinde barındıran bir katmandır. Örnek olarak ilgili projede sadece mail işlemleri için çalışmaktadır. Açılan modeller (poco classlar) Domain katmanında yer almaktadır. Bu nedenle Infrastructure projesi referanslarında Domain Katmanı bulunmaktadır.
### Presentation Katmanı
Bu katman dışarı veri göndermemizi veya almamızı sağlayan apiler veya form uygulamaları yada Web Siteler bu katmanda görev almaktadır. Bizim projemizde bu katmanda API bulunmaktadır.
### Test Bileşenleri
Bu katman aslında presentation katmanın bileşenlerindendir. Tüm Mimarinin eksiksiz ve Business Logic bakımından eksiksiz görevlerini yerine getirmektedir. İlgili projede xUnit ve MsUnit Testler bulunmaktadır. MsTest Web API request similasyonlarında kullanılmış olup service bileşeni için testler xUnit Test Olarak Yapılmıştır.
## Kullanılan Teknolojiler ve Yaklaşımlar
* [ASP.NET Core 7](https://learn.microsoft.com/tr-tr/aspnet/core/release-notes/aspnetcore-7.0?view=aspnetcore-7.0)
* [Entity Framework Core 7](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew)
* [Microsoft DI](https://learn.microsoft.com/tr-tr/dotnet/core/extensions/dependency-injection)
* [xUnit](https://xunit.net/)
* [Moq](https://github.com/moq)
* [MSTest](https://learn.microsoft.com/tr-tr/dotnet/core/testing/unit-testing-with-mstest)
* [GenericRepository](https://medium.com/@semihelitas/generic-repository-pattern-asp-net-core-e2d275ba0e)
* [OnionArchitecture](https://www.gencayyildiz.com/blog/nedir-bu-onion-architecture-tam-teferruatli-inceleyelim/)
* [EntityFramework InMemoryDatabase](https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli#supported-database-engines)
## Destek

Eğer sorunuz yada hata durumları oluşursa  [Yeni İş Tanımlaması](https://github.com/AhmetGuler1995/ReservationCase/issues/new) yapınız..
