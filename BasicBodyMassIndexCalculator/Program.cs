using System;
using System.Text.RegularExpressions;
using BasicBodyMassIndexCalculator.Common.Classes;

namespace BasicBodyMassIndexCalculator
{

    /*
        Vücut Kitle İndeksi hesaplayıcı 

        Senaryo: Kullanıcı sisteme giriş yaptıktan sonra adını, kilosunu ve boyunu konsol ekranı üzerinden girişini yapıyor.
        değerler arasında dikkat edilmesi gerekenler ise şöyle:
            - Adı sayısal ve semböl değerleri içeremez.
            - Boyu metre cinsinden olmalıdır.
            - Boyu realist olmak amacıyla 2 metre 99cm'den uzun olamaz.

        Sistem girilen değerler ile person değişken adı ile Person sınıfından yeni bir eleman türetir. Bu elemanın yapıcı değerler olarak
        kullanıcı ismi, kullanıcı boyu ve kullanıcı kilosunu alır. Bu değerler yapıcı elemana gönderilmeden önce sayısal değerlere dönüştürülürler.

        Oluşturulan kullanıcı Person sınıfındaki CalculateMassIndex metodunu kullanır, bu metod hakkında daha fazla bilgi Common/Classes/Person.cs dosyasında bulunuyor.
        CalculateMassIndex'den dönen değerin 10'dan büyük olması kişinin fazla kilosu olduğunu, 10'dan küçük olması ise çok zayıf olduğunu gösterir, son olarak bu koşullara
        göre ekrana mesaj yansıtılır.
     */

    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Vücut Kitle Endeksi Programı");

            Console.Write("Lütfen adınızı giriniz:");
            string personName = Convert.ToString(Console.ReadLine());

            
            // Burada Regex'den yararlandım çünkü kullanıcının girdiği isim değeri sayısal rakamlar içermemeli, semboller içermemeli, ilk karakter olarak boşluk almamalı
            // ancak boşluklu yapılıda olabilir çünkü ad soyad şeklinde giriş sağlayabilen kullanıcılar olabilir.

            // Burdaki koşullar için 3'e yakın if yazdıktan sonra onun yerine daha alternatif bir çözüm aradım yararlandığım kaynak adresini altta bulabilirsiniz.
            // https://stackoverflow.com/questions/9289451/regular-expression-for-alphabets-with-spaces
            if (!Regex.IsMatch(personName, "^[A-Za-z]+[A-Za-z ]*$"))
            {
                ValidationError("İsminiz geçersiz karakterler içeriyor. Lütfen geçerli bir isim giriniz.", personName, "Enes Bayraktar");
                return;
            }


            // Kullanıcının kilo değerini daha doğru bir şekilde doğrulayabilmem için String şeklidne aldım çünkü Convert.ToInt32 şekilnde aldığımda
            // sayı harici bir girdi girildiğinde hata fırlatıyordu.
            Console.Write("Lütfen kilonuzu giriniz:");
            string personWeight = Convert.ToString(Console.ReadLine());


            // Verilen string değerinin bir sayı olup olmadığını doğrulayabilmek için Int32.TryParse metodunu kullandım bu metod bool bir değer döndürdüğü için
            // if koşulunda rahatlıkla kullanabildim. numericValue ise C# 7.0 ile gelen ön tanımlı int tipi içeren bir değer. Bunun gibi doubleValue de mevcut.

            // Bu fonksiyonu Convert.Int32 de aldığım hatayı yaklayabilmek için kullandım. Kaynak olarak kullandığım adresi alta bulabilirsiniz.
            // https://www.arungudelli.com/tutorial/c-sharp/check-if-string-is-number/
            if (! Int32.TryParse(personWeight, out int numericValue))
            {
                ValidationError("Kilonuz sayılardan oluşmalıdır. Lütfen geçerli bir kilo giriniz.", personWeight, "110");
                return;
            }

            // Gerçekci değerler dışında bir kilo girilmesine karşın onaylama mekanizması ekledim. Kullanıcı değerini onaylarsa yine de program çalışmaya devam edicek.
            if (Convert.ToInt32(personWeight) > 600)
            {
                Console.WriteLine("Kilonuz realistik değerler dışında. Doğru giriş yaptınığınıza emin misiniz? (e/H)");
                if (! Console.ReadLine().Equals("e"))
                {
                    Console.WriteLine("Vücut kitle endeksi hesaplama iptal edildi. Teşekkür ederiz.");
                    return;
                }
            }

            // personWeight değişkeninde bahsettiğim nedenlerden olayı yine String olarak tercih etmek istedim.
            Console.Write("Lütfen boyunuzu metre cinsinden giriniz:");
            string personLength = Convert.ToString(Console.ReadLine());

            // personWeight değişkenine farklı olarak burada double tipi kontrolünü sağladım ek olarak ise double tipi tam sayıları içerdiği için
            // 189 gibi ben girilen sayının metre cinsinden olduğunu içerisinde girilen string değerinin . içerip içermediğiyle ilişkilendirdim.
            if (personLength.IndexOf(".") == -1 | !Double.TryParse(personLength, out double doubleValue))
            {
                ValidationError("Lütfen boyunuzu santimetre cinsinden giriniz.", personLength, "1.89");
                return;
            }


            // Burada ise dünyanın en uzun insanının boyunu düşündüğümde 3 metreden fazla boylu bir insan olamayacağı için saçma girdileri engellemek amaçlı
            // string tiğinde aldığım değerin ilk hanesini aldım ve 2'den büyük olup olmadığının kontrolünü sağladım.
            
            // Yukardaki kontrolü sağlayabilmek için Convert.ToInt32 ile string ifadeyi int'e çevirmem gerekti.

            // Ek olarak string de . ya kadar olan kısmı yani onluk kısmı alıp onun da değerini kontrol etmem gerekti çünkü bir kullanıcı yanlışlıkla 189.50 gibi değerler girebilir
            // hale geliyordu bu kontrolü yapmazsam. Bunun da önüne geçmek istedim.
            if (Convert.ToInt32(personLength.Substring(0, 1)) > 2 | Convert.ToInt32(personLength.Substring(0, personLength.IndexOf("."))) > 2)
            {
                ValidationError("Lütfen boyunuzu doğru giriniz.", personLength, "1.70");
                return;
            }

            // Burada yeni bir kişilik sınıfı elemanı oluşturuyorum. Bu sınıf yapıcı olarak isim, kilo ve boy değerlerini alıyor.
            // değerlerden boy double tipinde, kilo ise integer tipinde olmak zorunda.
            Person person = new Person(
                personName,
                Convert.ToInt32(personWeight),
                Convert.ToDouble(personLength)
            );

            // Oluşturduğumuz Person elemanın yardımcı metodu olan CalculateMassIndex'i çağırdık. Bu metod kullanıcının kilosundan boyunu çıkartıyor ve farkı değer olarak
            // dönüyor.
            int personMassIndex = person.CalculateMassIndex();

            // Dönen değerin 10'dan büyük ya da -10'dan küçük olmasıyla alakalı koşulların kontrolü ile referans değerlerle alakalı mesajları ekrana yansıttım.
            // böylelikle kullanıcın basitce kilo mu alması ya da vermesi gerektiği ile alakalı bilgi sahibi olması sağlandı.
            if (-10 <= personMassIndex && personMassIndex <= 10)
            {
                Console.WriteLine($"Sayın {person.Name} kilonuz normal");
            } else if (-10 >= personMassIndex)
            {
                Console.WriteLine($"Sayın {person.Name} kilonuz referans değerlerinin üstünde, kilo vermelisiniz.");
            } else
            {
                Console.WriteLine($"Sayın {person.Name} kilonuz referans değerlerin altında, kilo almalısınız.");
            }
            return;
        }

        // Komut satırı üzerinden girilen değerleri doğrulamada oluşan hata mesajları için tek tek Console.WriteLine yazmak istemedim bunun yerine
        // yardımcı metod olan ValidationError'ı oluşturdum, başta hata mesajını sonra hata verilen değeri ardından olması gereken tipte örnek değeri ekrana yansıtıyor.
        public static void ValidationError(string errorMessage, string errorValue, string defaultValue)
        {
            Console.WriteLine($"\nHata: {errorMessage}\nGirilen değer:{errorValue}\nÖrnek değer:{defaultValue}");
        }
    }
}
