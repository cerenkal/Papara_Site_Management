Bu Web projesi için: .Net 5 MVC, database için MS SQL Server, önyüz için bootstrap template giydirme kullanılmıştır.

Projede yönetici ve kullanıcı girişleri bulunmaktadır.
- Yönetici daire bilgilerini girer.
- Kullanıcı bilgilerini girer.Giriş yapması için otomatik olarak bir şifre oluşturulur. 
- Kullanıcıları dairelere atar
- Ay bazlı olarak aidat bilgilerini job girer.
- Ay bazlı olarak fatura bilgilerini job girer
- Kullanıcılar kredi kartı ile ödeme yapabilmesi için kredi kartı bilgilerini ekler.
- Projede her bir kullanıcı için banka bilgileri(kredi kartı no, CVV, son kullanma tarihi) girerek ödeme yapar ve kart bakiyesinden düşer.

Yönetici Yetkileri;
- Daire bilgilerini girebilir.
- Kullanıcı bilgilerini girer.
- Daire başına ödenmesi gereken aidat ve fatura bilgilerini girer(Aylık olarak). Toplu veya tek tek atama yapılabilir.
- Gelen ödeme bilgilerini görür.
- Gelen mesajları görür.
- Kişileri listeler, düzenler, siler.
- Daire/konut bilgilerini listeler, düzenler siler.
- Fatura ödemeyen kişilere günlük mail job ataması yapar.
- Kullanıcı listelerinin dökümünü pdf ya da excele aktarabilir.

Kullanıcı Yetkileri;
- Kendisine atanan fatura ve aidat bilgilerini görür.
- Kredi kartı bilgilerini girerek ödeme yapabilir.Ayrıca kart bilgilerini kaydedebilir.
- Yöneticiye mesaj gönderebilir.
- Profil sayfasından bilgilerini görüntüleyebilir ve email,telefon ve araç plakasını güncelleyebilir.
