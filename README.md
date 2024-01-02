# Etkinlik Yönetim Sistemi

Bu, C# ile uygulanmış basit bir konsol tabanlı Etkinlik Yönetim Sistemi projesidir. Proje, kullanıcıların ve organizasyonların etkinlikleri düzenlemesini ve katılımını kolaylaştırır. Temel varlıklar arasında `User`, `Participant`, `Organization` ve `Event` bulunmaktadır.

## İçindekiler

- [Giriş](#giriş)
- [Özellikler](#özellikler)
- [Kullanım](#kullanım)
- [Nesne Yönelimli Programlama Prensipleri](#nesne-yönelimli-programlama-prensipleri)
- [Geliştirmeler](#geliştirmeler)
- [Katkıda Bulunma](#katkıda-bulunma)
- [Lisans](#lisans)

## Giriş

Etkinlik Yönetim Sistemi, kullanıcılara, bireyler ve organizasyonlar tarafından etkinliklerin oluşturulması, yönetilmesi ve katılımı için bir ortam sağlar. Temel olarak, temel Nesne Yönelimli Programlama (OOP) prensiplerini sergilemek ve daha fazla genişlemeye olanak tanıyan bir temel sunan bir konsol uygulamasıdır.

## Özellikler

- **Kullanıcı Kimlik Doğrulama:** Kullanıcılar kimlik bilgileriyle sisteme giriş yapabilir.
- **Etkinlik İşlemleri:** Katılımcılar ve organizasyonlar tarafından etkinliklerin oluşturulması, görüntülenmesi ve silinmesi.
- **Katılım Yönetimi:** Katılımcılar etkinliklere katılabilir veya katılımı iptal edebilirler.

## Kullanım

Uygulamayı kullanmak için şu adımları izleyin:

1. Depoyu klonlayın: `git clone https://github.com/sizin-kullanıcı-adınız/etkinlik-yönetim-sistemi.git`
2. Proje dizinine gidin: `cd etkinlik-yönetim-sistemi`
3. Uygulamayı derleyip çalıştırın.

## Nesne Yönelimli Programlama Prensipleri

Proje, şu şekillerde OOP prensiplerine uygun olarak tasarlanmıştır:

1. **Kapsülleme:** Veri ve davranışlar, sınıflar içinde (`User`, `Participant`, `Organization`, `Event`) özel özellikler ve yöntemlerle kapsüllenmiştir.

2. **Miras Alma:** `Participant` ve `Organization` sınıfları, `User` sınıfından miras alır. Bu, kodun tekrar kullanılmasına olanak tanır ve "bir türdür" ilişkisini yansıtır.

3. **Polimorfizm:** `IParticipantEvent` ve `IOrganizationEvent` arayüzleri, `Participant` ve `Organization` sınıfları tarafından uygulanır; bu, etkinlikle ilgili eylemler için ortak bir arayüz sağlar.

4. **Soyutlama:** Arayüzler ve soyut sınıflar, sözleşmeleri tanımlamak ve ortak davranışları soyutlamak için kullanılır.
