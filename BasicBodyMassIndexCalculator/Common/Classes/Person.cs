using System;
using BasicBodyMassIndexCalculator.Common.Interfaces;

namespace BasicBodyMassIndexCalculator.Common.Classes
{
    /*
        Person sınıfı

        bu sınıf Vücut kitle endeksi hesaplamada sistemin ileride geliştirilip birden çok insanın aynı anda hesaplanması gerekeceği durumlara karşın hazırlandı.
        sınıf temel olarak IPerson arayüzünü kullanıyor, isim, kilo ve boy değişkenlerine sahip. Bu değişkenlerin düzenlenebilmesi için getter ve setterları mevcut.

        Vücut kitle endeksinin hesaplabilmesi için ise CalculateMassIndex isimli bir metod tanımladım. Bu metod basitce kullanıcının kilosundan boyunu çıkartıyor.
        Peki bu sınıf boyu double tipinden nasıl kilodan çıkartılacak referans değere ulaştırıyor dersek. Önce double tiğini Convert.ToIn32 ile integer değere dönüştürüyoruz.

        Bu sayede örnek değerimiz 1.89, 189 olarak tanımlanmış oluyor. Bu değerden 100cm yani 1 metre çıkarmamız ise denklemin referans değerini sağlayan parça oluyor.
        100cm çıkarmamızın sebebi ise bir insanın boy/kitle indeksi hesaplanırken 1.89m boyunda olan birinin 89+10 ile 89-10 aralığında olmasının normal değerler içinde kabul edilmesidir.
        2.10m olan birisi için bu değer 110+10 ila 110-10 arasında olması beni boy değerinden 1m çıkartmaya iten şey. Daha dinamik bir yolu bulunabilir mi bilmiyorum.
    */
    public class Person : IPerson
    {
        private string _name { get; set; }
        public string Name
        {
            get { return _name;  }
            set { _name = value; }
        }

        private int _weight { get; set; }
        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private double _length { get; set; }
        public double Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public Person(string name, int weight, double length)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Weight = weight;
            Length = length;
        }

        public int CalculateMassIndex()
        {
            return this.Weight - (
                Convert.ToInt32(this.Length) - 100
            );
        }

    }
}
