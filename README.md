# Etkinlik Yönetim Sistemi

Bu proje C# ile kodlanmış bir konsol uygulamasıdır. Etkinlik Yönetim Sistemi, organizasyonlar tarafından etkinliklerin oluşturulmasını, yönetilmesini ve kullanıcılar için de bu etkinliklere katılımı sağlar.
Proje, Nesne Yönelimli Programlama prensiplerini uygulayan bir konsol uygulamasıdır.

## Özellikler

- **Oturum Yönetimi:** Kullanıcı hesapları Organizasyon ve Katılımcı olmak üzere ikiye ayrılır. Kullanıcının seçimine göre oturum sağlanır. Oturum bilgilerini korumak için bir obje tutulur ve oturum sonlandırıldığında da bu obje temizlenir. Bu sayede katılımcı veya organizasyon hesapları arasında geçiş yapılması sağlanır.
- **Katılımcılar İçin Etkinlik İşlemleri:** Katılımcılar, Organizasyonlar tarafınfan düzenlenen tüm etkinlikleri görebilir, bir etkinliğin isim veya id bilgisi sayesinde o etkinliğe kaydolabilir ve etkinlik kayıtlarını iptal edebilirler.
- **Organizasyonlar İçin Etkinlik İşlemleri:** Organizasyonlar, tüm etkinlikleri görüntüleyebilir, kendi oluşturduğu etkinlikleri görüntüleyebilir, yeni etkinlikler oluşturup kaydedebilir, oluşturduğu etkinlikleri silebilir, oluşturduğu etkinliğe katılım sağlayacak kullanıcıların bir listesini görüntüleyebilirler.

## Gereksinimler

- Projeyi kendi bilgisayarınıza klonlayabilmek için `Git` sistemi indirilip kurulumu yapılmış olmalıdır.
- Projeyi çalıştırmak için `Microsoft Visual Studio` indirilip kurulumu yapılmış olmalıdır.

## Kullanım

Projeyi çalıştırmak için aşağıdaki adımları izleyebilirsiniz:
1. Komutları girmek için terminal ekranını açın ve `cd <dosya_adi>` komutuyla ilgili klasöre geçin.
2. Projeyi klonlayın: `https://github.com/Furkan-Boncuk/c-code.git`
3. Uygulamayı derleyip çalıştırın.

## Proje Detayları

Proje, OOP prensiblerine uygun hazırlanmıştır :

1. **Encapsulation (Kapsülleme):** Sınıfların içindeki veriler private olarak tanımlanmış ve get set metotlarıyla dışarıdan erişimi sağlanmıştır. Veri ve field'lar, sınıflar (`User`, `Participant`, `Organization`, `Event`, `AuthObject`) ,içerisinde kapsüllenmiştir.

2. **Inheritance (Miras):** `Participant` ve `Organization` sınıfları, `User` sınıfından miras alır. User içerisindeki field'ları ( Id, Username, Email, Password ) kendi içlerinde kullanırlar.

3. **Polymorphism (Çok Biçimlilik):** `IParticipantEvent` ve `IOrganizationEvent` interface'leri, `Participant` ve `Organization` sınıfları tarafından implemente edilir ve interface'ler içindeki metotlarda `overload` kullanılmıştır. Örneğin katılımcıların bir etkinliğe kayıt yapmasını sağlayan bir metot olan `public void AttendEvent(string eventName, Participant participantBody, List<Event> Events)` metodu bu kullanımla etkinlik isimlerine göre işlem yaparken aynı isimle tanımlanan `public void AttendEvent(int eventId, Participant participantBody, List<Event> Events)` metodu da etkinliklerin id bilgisine göre işlem yapar.
   
4. **Abstraction (Soyutlama):** `IParticipantEvent` ve `IOrganizationEvent` interface'leri, sınıflarda kullanılacak metotları belirleyip tanımlamak ve ortak işlemleri bir çatıda toplayarak soyutlama yapmak için kullanılır.

## Proje Ekran Görüntüleri 















 Temel varlıklar arasında `User`, `Participant`, `Organization` ve `Event` bulunmaktadır.
